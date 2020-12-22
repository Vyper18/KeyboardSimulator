using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace WpfApp6
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Скорость печатания, Количество ошибок, Количество пробелов в Tab
        int fails = 0, time = 0;
        const int tabSize = 4;

        bool ShiftPressed = false, CapsLockPressed = false;
        
        private List<Border> allButtons = new List<Border>();

        #region Таймер

        DispatcherTimer timer = new DispatcherTimer();

        private void dispatcherTimer_Tick(object sender, EventArgs e) => time++;
        #endregion

        // Словарь
        string[] arrWords =
        {
            "Hello", "World", "I", "am", "hello", "world", "play", "game", "watch", "films",
            "drink", "eat", "tea", "cofe", "black", "white", "yelow", "red", "blue", "angry",
            "milk", "Watch", "me", "send", "cake", "Japan", "Ukrain", "not", "hey", "member",
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "must", "generic", "content",
            "and", "man", "woman", "Player", "vote", "mouse", "apple", "angry", "fat"
        };

        // Contains
        public MainWindow()
        {
            InitializeComponent();

            labelSpeed.Content = "Speed: 0 chars/min";
            labelFails.Content = "Fails: 0";
            labelDificulty.Content = "Dificulty: 2";

            slider1.Value = 0;

            // Получение всех Border
            foreach (var i in gridKeybord.Children)
                allButtons.Add(i as Border);
            // Настрйка таймера
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        #region Увеличение и уменьшения шрифта

        private void KeyboardToUpper(UIElement child)
        {
            switch ((child as Label).Content)
            {
                #region Цифры
                case "`":
                    (child as Label).Content = "~";
                    break;
                case "1":
                    (child as Label).Content = "!";
                    break;
                case "2":
                    (child as Label).Content = "@";
                    break;
                case "3":
                    (child as Label).Content = "#";
                    break;
                case "4":
                    (child as Label).Content = "$";
                    break;
                case "5":
                    (child as Label).Content = "%";
                    break;
                case "6":
                    (child as Label).Content = "^";
                    break;
                case "7":
                    (child as Label).Content = "&";
                    break;
                case "8":
                    (child as Label).Content = "*";
                    break;
                case "9":
                    (child as Label).Content = "(";
                    break;
                case "0":
                    (child as Label).Content = ")";
                    break;
                case "-":
                    (child as Label).Content = "_";
                    break;
                case "=":
                    (child as Label).Content = "+";
                    break;
                #endregion
                #region Символы
                case "[":
                    (child as Label).Content = "{";
                    break;
                case "]":
                    (child as Label).Content = "}";
                    break;
                case ";":
                    (child as Label).Content = ":";
                    break;
                case "'":
                    (child as Label).Content = "\"";
                    break;
                case "/":
                    (child as Label).Content = "?";
                    break;
                case @"\":
                    (child as Label).Content = "|";
                    break;
                case ".":
                    (child as Label).Content = ">";
                    break;
                case ",":
                    (child as Label).Content = "<";
                    break;
                default:
                    if ((child as Label).Content.ToString().Length == 1)    // Все буквы в верхний регистр
                        (child as Label).Content = (child as Label).Content.ToString().ToUpper();
                    break;
                    #endregion
            }
        }

        private void KeyboardToLower(UIElement child)
        {
            switch ((child as Label).Content)
            {
                #region Цифры
                case "~":
                    (child as Label).Content = "`";
                    break;
                case "!":
                    (child as Label).Content = "1";
                    break;
                case "@":
                    (child as Label).Content = "2";
                    break;
                case "#":
                    (child as Label).Content = "3";
                    break;
                case "$":
                    (child as Label).Content = "4";
                    break;
                case "%":
                    (child as Label).Content = "5";
                    break;
                case "^":
                    (child as Label).Content = "6";
                    break;
                case "&":
                    (child as Label).Content = "7";
                    break;
                case "*":
                    (child as Label).Content = "8";
                    break;
                case "(":
                    (child as Label).Content = "9";
                    break;
                case ")":
                    (child as Label).Content = "0";
                    break;
                case "_":
                    (child as Label).Content = "-";
                    break;
                case "+":
                    (child as Label).Content = "=";
                    break;
                #endregion
                #region Символы
                case "{":
                    (child as Label).Content = "[";
                    break;
                case "}":
                    (child as Label).Content = "]";
                    break;
                case ":":
                    (child as Label).Content = ";";
                    break;
                case "\"":
                    (child as Label).Content = "'";
                    break;
                case "?":
                    (child as Label).Content = "/";
                    break;
                case "|":
                    (child as Label).Content = "\\";
                    break;
                case ">":
                    (child as Label).Content = ".";
                    break;
                case "<":
                    (child as Label).Content = ",";
                    break;
                default:
                    if ((child as Label).Content.ToString().Length == 1)
                        (child as Label).Content = (child as Label).Content.ToString().ToLower();
                    break;
                    #endregion
            }
        }

        #endregion

        // Сброс всех значений
        private void ResetAll() 
        {
            time = fails = 0;
            labelFails.Content = 0;
        }

        #region Кнопки
        // Генерация текста
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // Отключить Start
            buttonStart.IsEnabled = false;
            // Запуск таймера
            timer.Start();
            // Очистка текста
            labelString.Content = labelWriteText.Content = null;
            // Генерация текста
            for (int i = 0; i < slider1.Value; i++)
            {
                labelString.Content += arrWords[new Random(DateTime.Now.Millisecond).Next(arrWords.Length)] + " ";
                System.Threading.Thread.Sleep(5);
            }
            // Удаление в конце пробела
            labelString.Content = labelString.Content.ToString().Remove(labelString.Content.ToString().Length - 1, 1);
        }

        // Очистка текста и вывод результатов
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            // Отклбчение таймера
            timer.Stop();
            // Активация кнопки
            buttonStart.IsEnabled = true;
            // Очистка текста
            labelString.Content = labelWriteText.Content = null;
            ResetAll();
        }
        #endregion

        // Слайдер
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (labelDificulty != null)
            labelDificulty.Content = $"Dificulty: {Convert.ToInt32(slider1.Value)}";
        }

        #region Визуал нажатия кнопок и добавления их в текст
        private void KeyPressed(Border sender)
        {
            // Добавление буквы на labelWriteText
            switch (sender.Name.ToString())
            {
                #region Служебные клавиши
                case "Space":
                    labelWriteText.Content += " ";
                    break;
                case "Back":
                    if (labelWriteText.Content.ToString().Length > 0)
                        labelWriteText.Content = labelWriteText.Content.ToString().Remove(labelWriteText.Content.ToString().Length - 1, 1);
                    break;
                case "LeftShift":
                    ShiftPressed = true;
                    for (int i = 0; i < allButtons.Count; i++)
                        KeyboardToUpper(allButtons[i].Child);
                    break;
                case "RightShift":
                    ShiftPressed = true;
                    break;
                case "Capital":
                    CapsLockPressed = !CapsLockPressed;
                    for (int i = 0; i < allButtons.Count; i++)
                        KeyboardToUpper(allButtons[i].Child);
                    break;
                case "LeftCtrl":
                    break;
                case "RightCtrl":
                    break;
                case "LeftAlt":
                    break;
                case "RightAlt":
                    break;
                case "LWin":
                    break;
                case "RWin":
                    break;
                case "Tab":
                    labelWriteText.Content += " ".PadRight(tabSize);
                    break;
                case "Return":
                    if (labelWriteText.Content.Equals(labelString.Content))
                    {
                        MessageBox.Show(
                            $"Всё правильно\n" +
                            $"Слов: {Convert.ToInt32(slider1.Value)}\n" +
                            $"Ошибок: {fails}\n" +
                            $"Скорость набора символов: {((Convert.ToInt32(slider1.Value) - fails) / time) * 60}\n" +
                            $"Время: {time} секунд");
                        labelSpeed.Content = $"Speed: {Convert.ToInt32(slider1.Value)} char/min";
                        ResetAll();
                    }
                    break;
                case "OemQuestion":
                    labelWriteText.Content += (ShiftPressed || CapsLockPressed) ? "?" : "/";
                    break;
                #endregion
                // Буквы
                default:
                    labelWriteText.Content += (sender.Child as Label).Content.ToString();
                    break;
            }
            // Перекраска кнопки
            sender.Background = Brushes.Red;
            // Покраска labelString
            if (!sender.Name.ToString().Equals("Back"))
                PaintText();
        }

        private void KeyUnpressed(Border sender)
        {
            switch (sender.Name)
            {
                case "LeftShift":
                    ShiftPressed = false;
                    for (int i = 0; i < allButtons.Count; i++)
                        KeyboardToLower(allButtons[i].Child);
                    break;
                case "RightShift":
                    ShiftPressed = false;
                    break;
                case "Capital":
                    if (!CapsLockPressed)
                        for (int i = 0; i < allButtons.Count; i++)
                            KeyboardToLower(allButtons[i].Child);
                    break;
            }
            // Вернуть кнопку в исходное состояния
            sender.Background = (Brush)new BrushConverter().ConvertFromString($"{sender.Tag as string}");
        }

        #endregion

        #region Визуал текста
        // Поиск ошибок 
        private void PaintText()
        {
            try
            {
                if (labelWriteText.Content.ToString()[labelWriteText.Content.ToString().Length - 1].Equals(labelString.Content.ToString()[labelWriteText.Content.ToString().Length - 1]))
                    labelString.Foreground = labelWriteText.Foreground = Brushes.Green;
                else
                {
                    labelFails.Content = $"Fails: {++fails}";
                    labelString.Foreground = labelWriteText.Foreground = Brushes.Red;
                }
            }
            catch
            {
               
            }
        }
        #endregion

        #region События нажатия кнопок
        private void Border_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (Border i in allButtons)
                if (i.Name.Equals(e.Key.ToString()))
                    KeyUnpressed(i);
        }
        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Border i in allButtons)
                if (i.Name.Equals(e.Key.ToString()))
                    KeyPressed(i);
        }
        #endregion
    }
}
