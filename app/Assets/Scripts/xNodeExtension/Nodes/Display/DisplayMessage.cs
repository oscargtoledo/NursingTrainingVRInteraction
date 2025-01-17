using System.Collections;
using UnityEngine;
using NT.Atributes;
using NT.SceneObjects;
using TMPro;

namespace NT.Nodes.Display {
    
    public class DisplayMessage : FlowNode {

        [NTInput] public string messageText;

        [NTInput] public SceneGameObjectReference objectPosition;
        //[Input(ShowBackingValue.Never, ConnectionType.Override)] public SceneGameObjectReference objectPosition;
        

        public object GetValue() {
            return null;
        }

        public override IEnumerator ExecuteNode(NodeExecutionContext context)
        {
            GameObject messageGameObject = GameObject.Find("ShowMessage/Bocadillo/MessageText");
            GameObject parentMessageGameObject = getGameObjectParent(getGameObjectParent(messageGameObject));

            // Check if message is empty
            string message = GetInputValue<string>(nameof(this.messageText), this.messageText);
            bool visible = !string.IsNullOrEmpty(message);
            // Modify visibility by changing scale
            Vector3 scale = visible ? Vector3.one : Vector3.zero;
            parentMessageGameObject.transform.localScale = scale;

            if (visible)
            {
                TextMeshPro textComponent = messageGameObject.GetComponent<TextMeshPro>();
                textComponent.text = message;
                // Place message outside the object's bounding box, towards the scene center (to avoid walls collision)
                GameObject positionGameObject = GetInputValue<SceneGameObject>(nameof(objectPosition), null).gameObject;
                BoxCollider collider = positionGameObject.gameObject.GetComponentInChildren<BoxCollider>();

                if (collider != null)
                {
                    Vector3 colliderCenter = collider.transform.TransformPoint(collider.center);
                    colliderCenter.y = 0;
                    parentMessageGameObject.transform.position = colliderCenter;

                    Vector3 offset = collider.size;
                    offset.x += 0.5f;  // Message size fixed offset
                    offset.z = 0;
                    parentMessageGameObject.transform.Translate(offset);
                }
                else
                {
                    parentMessageGameObject.transform.position = positionGameObject.transform.position;
                    parentMessageGameObject.transform.Translate(new Vector3(1, 1, 0));
                }

                // Rotaci�n de seguimiento alrededor del objeto de posici�n??
            }

            yield return null;
        }

        private GameObject getGameObjectParent(GameObject g)
        {
            return g.transform.parent.gameObject;
        }

        public override string GetDisplayName(){
            return "Display Message";
        }
    }
}
