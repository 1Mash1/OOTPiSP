using System;
using System.Windows.Forms;
using MyShape; 

namespace CaesarEncryptionPlugin
{
    public class CaesarProcessor : IDataProcessorPlugin
    {
        // Plugin name displayed in the UI menu
        public string Name => "Шифр Цезаря";

        // Default shift value for byte rotation
        private byte _shift = 10;

        // Encrypts data by adding the shift value to each byte
        public byte[] ProcessBeforeSave(byte[] data)
        {
            if (data == null)
                return null;

            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                // Bytes will naturally overflow/wrap around 255
                result[i] = (byte)(data[i] + _shift);
            }
            return result;
        }

        // Decrypts data by subtracting the shift value from each byte
        public byte[] ProcessAfterLoad(byte[] data)
        {
            if (data == null)
                return null;

            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                // Reverse the shift to restore original byte values
                result[i] = (byte)(data[i] - _shift);
            }
            return result;
        }

        // Displays a dialog to modify the encryption shift value
        public void ShowSettings()
        {
            // Requires reference to Microsoft.VisualBasic assembly
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите значение (0-255):",
                "Настройки",
                _shift.ToString());

            if (byte.TryParse(input, out byte newShift))
            {
                _shift = newShift;
                MessageBox.Show($"Сдвиг обновлен до: {_shift}", "Успешно");
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Неверный ввод! Введите число от 0 до 255.", "Неверный ввод");
            }
        }
    }
}