using System.Collections;
using UnityEngine;
namespace Hybriona
{
    public class Wait
    {
        //new WaitForSeconds
        public static IEnumerator WaitForSeconds(float wait)
        {
            float timer = 0;
            while (timer <= wait)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }


        // new WaitForSecondsRealtime
        public static IEnumerator WaitForSecondsRealtime(float wait)
        {
            float timer = 0;
            while (timer <= wait)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        // new WaitForSecondsRealtime
        public static IEnumerator WaitForFixedSeconds(float wait)
        {
            float timer = 0;
            while (timer <= wait)
            {
                timer += Time.fixedDeltaTime;
                yield return null;
            }
        }
    }
}