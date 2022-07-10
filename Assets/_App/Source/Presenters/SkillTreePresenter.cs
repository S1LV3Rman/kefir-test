namespace Source
{
    public class SkillTreePresenter : IInitPresenter
    {
        private readonly ISkillTreeView _skillTreeView;
        private readonly SkillTreeModel _skillTreeModel;

        public SkillTreePresenter(ISkillTreeView skillTreeView, SkillTreeModel skillTreeModel)
        {
            _skillTreeView = skillTreeView;
            _skillTreeModel = skillTreeModel;
        }

        public void Init()
        {
            CreateTreeView();

            _skillTreeModel.OnSkillsStatusChanged += UpdateView;
        }

        private void CreateTreeView()
        {
            _skillTreeView.ClearAllSkillsViews();
            _skillTreeView.ClearAllConnectionsViews();

            var skillsGraph = _skillTreeModel.SkillsGraph;
            var createdConnections = skillsGraph.ToConnectionsGraph<bool>(false);
            foreach (var skill in skillsGraph)
            {
                _skillTreeView.CreateSkillView(skill.Value.Info);
                foreach (var connectedSkillId in skill.Value.ConnectedSkillsId)
                {
                    if (createdConnections[skill.Key][connectedSkillId]) continue;

                    var connectedSkill = skillsGraph[connectedSkillId].Info;
                    _skillTreeView.CreateConnectionView(skill.Value.Info, connectedSkill);

                    createdConnections[skill.Key][connectedSkillId] = true;
                    createdConnections[connectedSkillId][skill.Key] = true;
                }
            }
            
            UpdateView();
        }

        private void UpdateView()
        {
            var skillsGraph = _skillTreeModel.SkillsGraph;
            var connectionsStatus = skillsGraph.ToConnectionsGraph(ConnectionStatus.Undefined);
            
            foreach (var skill in skillsGraph)
            {
                _skillTreeView.SetSkillStatus(skill.Value.Info, skill.Value.Status);
                foreach (var connectedSkillId in skill.Value.ConnectedSkillsId)
                {
                    var connectedSkill = skillsGraph[connectedSkillId];
                    if (connectionsStatus[skill.Key][connectedSkillId] != ConnectionStatus.Undefined)
                        continue;

                    ConnectionStatus connectionStatus;
                    if (skill.Value.Status == SkillStatus.Learned && connectedSkill.Status == SkillStatus.Learned)
                        connectionStatus = ConnectionStatus.Learned;
                    else if (skill.Value.Status == SkillStatus.Learned || connectedSkill.Status == SkillStatus.Learned)
                        connectionStatus = ConnectionStatus.Learnable;
                    else
                        connectionStatus = ConnectionStatus.Blocked;

                    _skillTreeView.SetConnectionStatus(skill.Value.Info, connectedSkill.Info, connectionStatus);
                    
                    connectionsStatus[skill.Key][connectedSkillId] = connectionStatus;
                    connectionsStatus[connectedSkillId][skill.Key] = connectionStatus;
                }
            }
        }
    }
}