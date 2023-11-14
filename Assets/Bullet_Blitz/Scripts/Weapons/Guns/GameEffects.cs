using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffects : MonoBehaviour
{
  
    [SerializeField] private  Material weaponTracerMaterial;
    [SerializeField] private  Sprite shootFlashSprite;
    public static GameEffects Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    public  void ShootEffects(Vector3 gunEndPointPosition, Vector3 shootPosition)
    {
        CreateWeaponTracer(gunEndPointPosition, shootPosition);
        CreateShootFlash(gunEndPointPosition);
        //ShakeCamera(.5f, .05f);
    }
    public void CreateShootFlash(Vector3 spawnPosition)
    {
        World_Sprite worldSprite = World_Sprite.Create(spawnPosition, shootFlashSprite);
        FunctionTimer.Create(worldSprite.DestroySelf, .1f);
    }
    private   void CreateWeaponTracer(Vector3 fromPosition,Vector3 targetPosition)
    {
        Vector3 dir = (targetPosition - fromPosition).normalized;
        float eulerz = UtilsClass.GetAngleFromVectorFloat(dir) - 90;
        float distance = Vector3.Distance(fromPosition, targetPosition);
        Vector3 tracerSpawnPosition = fromPosition + dir * distance * 0.5f;
        Material tmpWeaponTracerMaterial = new Material(weaponTracerMaterial);
        tmpWeaponTracerMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance / 25f));
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPosition, eulerz, 0.5f, distance, tmpWeaponTracerMaterial, null, 10000);
        int frame = 0;
        float framerate = .016f;
        float timer = framerate;
        worldMesh.SetUVCoords(new World_Mesh.UVCoords(0, 0, 16, 256));
        FunctionUpdater.Create(() => {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                frame++;
                timer += framerate;
                if (frame >= 4)
                {
                    worldMesh.DestroySelf();
                    return true;
                }
                else
                {
                    worldMesh.SetUVCoords(new World_Mesh.UVCoords(16 * frame, 0, 16, 256));
                }
            }
            return false;
        });
    }
    public static void ShakeCamera(GameObject virtualCamera, float intensity, float timer)
    {
        Vector3 lastCameraMovement = Vector3.zero;
        FunctionUpdater.Create(delegate () {
            timer -= Time.unscaledDeltaTime;
            Vector3 randomMovement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * intensity;
            virtualCamera.transform.position -= lastCameraMovement + randomMovement;
            lastCameraMovement = randomMovement;
            return timer <= 0f;
        }, "CAMERA_SHAKE");
    }
}
