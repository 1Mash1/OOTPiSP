using System;
using System.Windows.Forms;
using MyShape; 

namespace XorEncryptionPlugin
{
    public class XorProcessor : IDataProcessorPlugin
    {
        // Plugin name for the UI menu
        public string Name => "Шифрование xor";

        // Secret key for encryption (part of 10-point task)
        private byte _key = 42;

        // Encrypts data by XORing each byte
        public byte[] ProcessBeforeSave(byte[] data)
        {
            return ApplyXor(data);
        }

        // Decrypts data (XOR again restores original values)
        public byte[] ProcessAfterLoad(byte[] data)
        {
            return ApplyXor(data);
        }

        // Simple XOR logic
        private byte[] ApplyXor(byte[] data)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ _key);
            }
            return result;
        }

        // Settings window to change the key (10-point requirement)
        public void ShowSettings()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введите значение длины ключа (0-255):", "Настройки", _key.ToString());
            if (byte.TryParse(input, out byte newKey))
            {
                _key = newKey;
                MessageBox.Show("Ключ обновлен до: " + _key);
            }
        }
    }
}