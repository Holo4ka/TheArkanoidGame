using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidGame
{
    public class StaticBlock : Platform
    {
        public Color BorderColor { get; set; }

        /// <summary>
        /// Цвет заливки ("тела") статичного блока
        /// </summary>
        public Color BodyColor { get; set; }

        /// <summary>
        /// Количество необходимых ударов шариком по статичному блоку, чтобы
        /// его уничтожить
        /// </summary>
        public int HitsToDestroy { get; set; }

        /// <summary>
        /// Счётчик произведённых ударов шариком по статичному блоку
        /// </summary>
        public int CurrentHits { get; set; } = 0;

        /// <summary>
        /// Конструктор. Создаёт экземпляр статичного блока
        /// </summary>
        /// <param name="title">название статичного блока</param>
        /// <param name="width">ширина блока, в пикселях</param>
        /// <param name="height">высота блока, в пикселях</param>
        public StaticBlock(string title, int width, int height) : base(title, width, height)
        {
        }
    }
}
