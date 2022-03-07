
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class ExecuteTrajectoryActionGoalSubscriber : UnitySubscriber<MessageTypes.Moveit.ExecuteTrajectoryActionGoal>
    {
        public List<string> JointNames;
        public List<JointStateWriter> JointStateWriters;
        public MessageTypes.Moveit.ExecuteTrajectoryActionGoal executeTrajectoryActionGoal;
        protected override void ReceiveMessage(MessageTypes.Moveit.ExecuteTrajectoryActionGoal message)
        {
            executeTrajectoryActionGoal = message;
        }
    }
}

