using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class TFMessageSubscriber : UnitySubscriber<MessageTypes.Tf2.TFMessage>
    {
        [SerializeField] TFMessageWriter[] tFMessageWriters;

        protected override void ReceiveMessage(MessageTypes.Tf2.TFMessage message)
        {
            Vector3 rot = Vector3.zero;
            Debug.Log("Message Received");
            int index;
            MessageTypes.Geometry.Transform translation;
            string deb = "";
            for (int i = 0; i < message.transforms.Length; i++)
            {
                deb += message.transforms[i].header.frame_id;
                translation = message.transforms[i].transform;
                index = 0;
                while (message.transforms[i].header.frame_id != tFMessageWriters[index].Frame_id) 
                {
                    index++;
                    if (index>=message.transforms.Length)
                    {
                        index = -1;
                        break;
                    }
                }
                Debug.Log(index);
                if (index != -1) 
                {
                    tFMessageWriters[index].Translation = TransformExtensions.Ros2Unity(new UnityEngine.Vector3((float)translation.translation.x, (float)translation.translation.y, (float)translation.translation.z));
                    Vector3 rot2 = new Quaternion((float)translation.rotation.x, (float)translation.rotation.y, (float)translation.rotation.z, (float)translation.rotation.w).eulerAngles;
                    rot.x += rot2.x;
                    rot.y += -rot2.z;
                    rot.z += rot2.y;
                    tFMessageWriters[index].Rotation = UnityEngine.Quaternion.Euler(rot);
                }
            }
        }
    }
}