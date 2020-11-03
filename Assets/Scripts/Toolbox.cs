using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using Simplenoid.Controllers;
using Simplenoid.Helpers;
using Simplenoid.Interface;

namespace Simplenoid
{
    /// <summary>
    /// Главный объект управления
    /// Полка с контроллерами(в некотором смысле "Сервис локатор")
    /// </summary>
    public sealed class Toolbox : Singleton<Toolbox>
    {
        [SerializeField] Dictionary<int, BaseController> _controllers = new Dictionary<int, BaseController>();
        private GameObject _controllersGO;
        /// <summary>
        /// Настройка сцены
        /// </summary>
        public void Setup()
        {
            ClearScene();
            _controllersGO = new GameObject { name = "Controllers" };
        }
        /// <summary>
        /// Очистка сцены
        /// </summary>
        public void ClearScene()
        {
            if (_controllersGO)
            {
                Destroy(_controllersGO);
            }
            _controllers.Clear();
        }

        /// <summary>
        /// Добавление объекта контроллера "на полку"
        /// </summary>
        /// <typeparam name="T">Тип контроллера</typeparam>
        /// <returns>Контроллер</returns>
        public T Add<T>() where T : BaseController
        {
            BaseController o;
            var hash = typeof(T).GetHashCode();
            if (Instance._controllers.TryGetValue(hash, out o))
                return (T)o;

            BaseController controller = _controllersGO.AddComponent<T>();
            var awakeble = controller as IAwake;
            if (awakeble != null) awakeble.OnAwake();
            Instance._controllers.Add(hash, controller);
            return (T)controller;
        }
        /// <summary>
        /// Возврат контроллера "с полки"
        /// </summary>
        /// <typeparam name="T">Тип контроллера</typeparam>
        /// <returns>Контроллер</returns>
        public T Get<T>() where T : BaseController
        {
            BaseController resolve;
            Instance._controllers.TryGetValue(typeof(T).GetHashCode(), out resolve);
            return (T)resolve;
        }
        /// <summary>
        /// Удаление Контроллера
        /// </summary>
        /// <typeparam name="T">Тип контроллера</typeparam>
        public void Remove<T>() where T : BaseController
        {
            if (ApplicationIsQuitting) return;
            Instance._controllers.Remove(typeof(T).GetHashCode());
        }

        private Toolbox() { }
    }
}