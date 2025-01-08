using UnityEngine;
using System;
using Zenject;
using UI;

namespace Game
{
    public class GlobalDependencyInstaller : MonoInstaller
    {
        [SerializeField] private UIFactory _uiFactory;

        public override void InstallBindings()
        {
            Container.Bind<UIFactory>().FromInstance(_uiFactory);
        }
    }
}