using UnityEngine;

namespace OguzFan_Mechanic
{
     public class PivotGizmos : MonoBehaviour
     {
          //Pivot noktasını kolayca görmek için
          [SerializeField] private float gizmoSize = .7f;
          [SerializeField] private Color gizmoColor = Color.magenta;

          private void OnDrawGizmos()
          {
               Gizmos.color = gizmoColor;
               Gizmos.DrawWireSphere(transform.position, gizmoSize);
          }
     }
}
