
using UnityEngine;

public class RecoilSystem : MonoBehaviour
{
    //rotation
    Vector3 trargetRotation;
    Vector3 currentRotation;
    
    public void Recoil(float snappiness, float returnSpeed)
    {
            trargetRotation = Vector3.Lerp(trargetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
            currentRotation = Vector3.Lerp(currentRotation, trargetRotation, snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire(float recoilX, float recoilY, float recoilZ)
    { 
        trargetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));  
    }
}
