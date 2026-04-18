using System;

namespace MyShape
{
    public interface IDataProcessorPlugin
    {
        string Name { get; } // Plugin name for the UI menu

        byte[] ProcessBeforeSave(byte[] data); // Encrypts or processes data before saving to file

        byte[] ProcessAfterLoad(byte[] data);  // Decrypts or restores data after loading from file

        void ShowSettings(); // Opens a window for plugin settings (e.g. password input)
    }
}