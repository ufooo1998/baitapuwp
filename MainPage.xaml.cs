using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Demo2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private user current_user;
        public MainPage()
        {
            this.current_user = new user();
            this.InitializeComponent();
        }

        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            this.current_user.username = this.UserName.Text;
            this.current_user.phone = this.Phone.Text;
            this.current_user.email = this.Email.Text;
            
            string jsonInfoString = JsonConvert.SerializeObject(this.current_user);

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";
            StorageFile file = await savePicker.PickSaveFileAsync();
            await FileIO.WriteTextAsync(file, jsonInfoString);
        }

        private async void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Users expect to have a filtered view of their folders 
            openPicker.FileTypeFilter.Add(".txt");
            // Open the picker for the user to pick a file
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null){
                string data = await FileIO.ReadTextAsync(file);
                user loadUserInfo = JsonConvert.DeserializeObject<user>(data);
                this.UserName.Text = loadUserInfo.username;
                this.Phone.Text = loadUserInfo.phone;
                this.Email.Text = loadUserInfo.email;
            } 
        }
    }
}
