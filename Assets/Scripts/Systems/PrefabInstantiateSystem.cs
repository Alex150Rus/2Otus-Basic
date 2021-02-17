using Entitas;
using System.Collections.Generic;
using UnityEngine;

//реактивная система. <для каких сущностей будет использоваться>. Она должна реализовать три метода
//Первые два метода объясняют на какие события должна реагировать наша реактивная система
public class PrefabInstantiateSystem : ReactiveSystem<GameEntity>
{
    Contexts contexts;

    public PrefabInstantiateSystem(Contexts contexts)
        : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        //говорим, что реактивная система должна реагировать на появление/удаление компонента prefab
        //в GameController в Awake есть вызов метода entity.AddPrerfab(playerPrefab). В этот момент Entitas
        //поймёт, что нужно активировать нашу систему. Сразу он её не вызовет. Он запомнит, 
        //что её нужно активировать для этой Entity и сделает это в следующем цикле обновления
        //в следующем Execute (либо в этом кадре, либо в следующем, в зависимости от порядка систем).
        //В целом, нужно понимать, что это происходит не сразу как мы добавляем компонент, а чуть позже.
        //Добавление компонента помечает во внутренних структурах Entitas, что нужно вызвать реактивную систему
        return context.CreateCollector(GameMatcher.Prefab);
    }

    //позволяет дополнительно отфильтровать сущности по заданному нами условию.
    // если в Гет триггер можно добавлять простые условия по наличию/отсутствию компонента, то
    //здесь можно писать обысный С# код и задавать более сложные условия (если поле равно чему-то, 
    //если выполняются какие-то внешние условия.
    protected override bool Filter(GameEntity entity)
    {
        //если у сущности есть view, то нет смысла инстаншировать ещё один
        return entity.hasPrefab && !entity.hasView;
    }

    //выполняет нашу реактивную систему. Сюда приходит список сущностей, которые поменялись за прошедший кадр
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
            e.AddView(GameObject.Instantiate(e.prefab.prefab));
    }
}
