using System.Collections;
using TMPro;
using UnityEngine;

namespace LineOfSight
{
    public class LineOfSightBehaviour : MonoBehaviour
    {
        //Que tan lejos puede ver
        public float range;
        //Que tan amplia es su vision
        public float angle;

        
        public TMP_Text countDownText;

        public Canvas countDownCanvas;
        public string countDownEndMessage;
        public Transform target;
        public int countDown;
        public int maxCountDown;
        private bool isCalled = false;
        private GameObject player;
        public GameObject enemyIndicator;
        

        //Que cosas puede ver y/o obstruyen su vision
        public LayerMask visibles = ~0;//Hack para decir "TODOS"

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
           
        }
        private void Start()
        {
            countDown = 0;
        }

        private void Update()
        {
            if (!isCalled) 
            {
                StartCoroutine(CountDownCo());
            }            
        }

        public bool IsInSight(Transform target)
        {
            
            //Vector entre la posicion del target y mi posicion
            var positionDiference = player.transform.position - transform.position;
            //Distancia al target
            var distance = positionDiference.magnitude;

            // - Si esta mas lejos que mi rango => no lo veo
            if (distance > range) return false;

            //Angulo entre mi forward y la direccion al target
            var angleToTarget = Vector3.Angle(transform.forward, positionDiference);

            // - Si el angulo es mayor a la mitad de mi angulo maximo => no lo veo
            if (angleToTarget > angle / 2) return false;
            //Por qué dividimos por 2?
            //El angulo de vision lo tomamos entre extremo y extremo del rango,
            //en cabio el angulo al target entre el centro y el el extremo.

            // - Tiramos un rayo para chequear que no haya nada obstruyendo la vista.
            RaycastHit hitInfo;//En caso de haber una colision en el rayo aca se guarda la informacion

            if (Physics.Raycast(transform.position, positionDiference, out hitInfo, range, visibles))
            {
                //Si entra aca => colisiono con algo => hay algo obstruyendo
                //>>PERO<< tenemos que chequea que ese algo no sea el objeto que queremos ver
                if (hitInfo.transform != target) return false;
            }

            //Si no paso nada de lo anterior, el objeto esta a la vista
            return true;
        }



        private IEnumerator CountDownCo()
        {
            isCalled = true;
            
            if (IsInSight(player.transform))
            {
                countDown++;
                countDownCanvas.gameObject.SetActive(true);
                enemyIndicator.SetActive(true);
                countDownText.text = countDown.ToString();
            }
            else
            {
                countDown = 0;
                countDownCanvas.gameObject.SetActive(false);
                
            }

            if (countDown <= maxCountDown)
            {
                yield return new WaitForSeconds(1f);
            }

            isCalled = false;
            enemyIndicator.SetActive(false);
           
            

            if (countDown >= maxCountDown)
            {
                Debug.Log("Count Down Finished");
                countDownCanvas.gameObject.SetActive(false);
                countDown = 0;
                player.GetComponent<PlayerOneScript>().ForceDie();
            }
               
          
        }

        void OnDrawGizmos()
        {  
            //Replica graficamente los calculos de arriba
            var position = transform.position;

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(position, range);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(position, position + Quaternion.Euler(0, angle / 2, 0) * transform.forward * range);
            Gizmos.DrawLine(position, position + Quaternion.Euler(0, -angle / 2, 0) * transform.forward * range);
        }

    }
}
