using Entitas;
using UnityEngine;

//Без этой системы, после уничтожения нашей сущности в коде, объект на сцене остаётся.
//Эта система его убирает. View - представление нашей сущности в Unity

//Нюанс. Когда реактивная система вызывается при удалении компонента мы не можем получить доступ
//к содержимому этого компонента, а нам нужен GO для уничтожения.
public class ViewDestroySystem : IInitializeSystem, ITearDownSystem
{
    IGroup<GameEntity> group;

    public ViewDestroySystem(Contexts contexts)
    {
        //берём группу сущностей, у которых есть компонент View
        group = contexts.game.GetGroup(GameMatcher.View);
    }

    public void Initialize()
    {
        //подписываемся на событие, которое вызывается до того как сущность уничтожена и компонент удалён
        group.OnEntityRemoved += OnViewRemoved;
    }

    //завершение работы контекста
    public void TearDown()
    {
        //отписываемся от события, подчищаем за собой
        group.OnEntityRemoved -= OnViewRemoved;
    }

    // вызываетмя перед тем как компонент будет удалён
    void OnViewRemoved(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
    {
        //кастуем - осуществляем преобразование типов данных)). Т.к. знаем, что наша группа работает только с View
        var view = (ViewComponent)component;
        GameObject.Destroy(view.gameObject);
    }
}
