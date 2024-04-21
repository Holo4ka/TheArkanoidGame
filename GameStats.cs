using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public class GameStats
    {
        public int NumberOfWins { get; set; }

        /// <summary>
        /// количество поражений в игре
        /// </summary>
        public int NumberOfFailures { get; set; }

        /// <summary>
        /// Пользовательские игровые счётчики (произвольные).
        /// Позволяют вести различную статистику в игре
        /// </summary>
        private Dictionary<string, int> counters;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameStats()
        {
            ResetAll();
        }

        /// <summary>
        /// Добавить новый игровой счётчик к объекту игровой статистики
        /// </summary>
        /// <param name="counterKey">название счётчика</param>
        /// <param name="initalValue">начальное значение счётчика</param>
        /// <returns>true, если счётчик был добавлен к объекту игровой статистики, иначе false</returns>
        public bool AddGameCounter(string counterKey, int initalValue)
        {
            if (counterKey == null)
            {
                return false;
            }
            if (!counters.ContainsKey(counterKey))
            {
                counters.Add(counterKey, initalValue);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Сбросить показания указанного игрового счётчика
        /// </summary>
        /// <param name="counterKey">название счётчика для сброса его значений</param>
        /// <returns>true, если счётчик существует, и его значение было установлено в 0</returns>
        public bool ResetGameCounter(string counterKey)
        {
            if (counterKey == null)
            {
                return false;
            }
            if (counters.ContainsKey(counterKey))
            {
                counters[counterKey] = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Увеличить на единицу заданный игровой счётчик
        /// </summary>
        /// <param name="counterKey">игровой счётчик, значение которого нужно увеличить на единицу</param>
        /// <returns>true, если счётчик существует, и его значение было увеличено на 1</returns>
        public bool IncrementGameCounter(string counterKey)
        {
            if (counterKey == null)
            {
                return false;
            }
            if (counters.ContainsKey(counterKey))
            {
                counters[counterKey]++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Установить заданное значение для игрового счётчика
        /// </summary>
        /// <param name="counterKey">игровой счётчик, значение которого нужно изменить</param>
        /// <param name="value">новое значение для счётчика</param>
        /// <returns>true, если счётчик существует, и его значение было изменено на указанное в параметре <paramref name="value"/></returns>
        public bool SetGameCounterValue(string counterKey, int value)
        {
            if (counterKey == null)
            {
                return false;
            }
            if (counters.ContainsKey(counterKey))
            {
                counters[counterKey] = value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Получить значение игрового счётчика
        /// </summary>
        /// <param name="counterKey">игровой счётчик, значение которого нужно получить</param>
        /// <returns>возвращает значение игрового счётчика, если он существует, в противном случае вернёт -1</returns>
        public int GetGameCounterValue(string counterKey)
        {
            if (counterKey == null)
            {
                return -1;
            }
            if (counters.ContainsKey(counterKey))
            {
                return counters[counterKey];
            }
            return -1;
        }

        /// <summary>
        /// Увеличить количество успешных партий/побед/выигрышей в игре
        /// </summary>
        public void IncrementNumberOfWins()
        {
            NumberOfWins++;
        }

        /// <summary>
        /// Увеличить количество проигрышных партий/проигрышей/поражений в игре
        /// </summary>
        public void IncrementNumberOfFailures()
        {
            NumberOfFailures++;
        }

        /// <summary>
        /// Обнулить количество успешных партий/побед/выигрышей в игре
        /// </summary>
        public void ResetNumberOfWins()
        {
            NumberOfWins = 0;
        }

        /// <summary>
        /// Обнулить количество проигрышных партий/проигрышей/поражений в игре
        /// </summary>
        public void ResetNumberOfFailures()
        {
            NumberOfFailures = 0;
        }

        /// <summary>
        /// Сбросить все существующие игровые счётчики в 0.
        /// Если словарь счётчиков ещё не был создан, то он будет впервые создан при вызове метода.
        /// </summary>
        public void ResetAllGameCounters()
        {
            if (counters == null)
            {
                counters = new Dictionary<string, int>();
                return;
            }

            if (counters.Count > 0)
            {
                foreach (string counterKey in counters.Keys)
                {
                    counters[counterKey] = 0;
                }
            }

        }

        /// <summary>
        /// Сбросить все показания текущего объекта игровой статистики
        /// </summary>
        public void ResetAll()
        {
            ResetAllGameCounters();
            ResetNumberOfWins();
            ResetNumberOfFailures();
        }
    }
}
