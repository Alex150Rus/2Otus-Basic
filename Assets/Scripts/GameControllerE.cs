using UnityEngine;
using Entitas;

public class GameControllerE : MonoBehaviour
{
    Systems systems;
    public GameObject playerPrefab;
    void Awake()
    {
        var contexts = Contexts.sharedInstance;

        systems = new Systems();
        systems.Add(new DeathSystem(contexts));
        systems.Add(new PrefabInstantiateSystem(contexts));
        //systems.Add(new ViewDestroySystem(contexts));
        //systems.Add(new PlayerInputSystem(contexts));
        //systems.Add(new ForwardMovementSystem(contexts));
        //systems.Add(new ShotCollisionSystem(contexts));
        //systems.Add(new TransformApplySystem(contexts));
        systems.Initialize();

        var entity = contexts.game.CreateEntity();
        entity.isPlayer = true;
        entity.AddHealth(3.0f);
        entity.AddPrefab(playerPrefab);

    }

    void OnDestroy()
    {
        systems.TearDown();
    }

    void Update()
    {
        systems.Execute();
        systems.Cleanup();
    }
}