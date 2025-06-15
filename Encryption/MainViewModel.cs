using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace Encryption
{
    public class MainViewModel : BindableBase
    {
        #region Private Fields

        private string? password;
        private string? fileToEncryptFilePath;
        private string? fileToDecryptFilePath;
        private string? directoryToEncrypt;
        private string? directoryToDecrypt;
        private ObservableCollection<string>? log;

        #endregion

        #region Public Properties

        public string? Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public string? FileToEncryptFilePath
        {
            get { return fileToEncryptFilePath; }
            set { SetProperty(ref fileToEncryptFilePath, value); }
        }

        public string? FileToDecryptFilePath
        {
            get { return fileToDecryptFilePath; }
            set { SetProperty(ref fileToDecryptFilePath, value); }
        }
        
        public string? DirectoryToEncrypt
        {
            get { return directoryToEncrypt; }
            set { SetProperty(ref directoryToEncrypt, value); }
        }

        public string? DirectoryToDecrypt
        {
            get { return directoryToDecrypt; }
            set { SetProperty(ref directoryToDecrypt, value); }
        }

        public ObservableCollection<string>? Log
        {
            get { return log; }
            set { SetProperty(ref log, value); }
        }

        #endregion

        #region Commands

        public ICommand FindFileToEncryptCommand { get; private set; }
        public ICommand FindFileToDecryptCommand { get; private set; }
        public ICommand FindDirectoryToEncryptCommand { get; private set; }
        public ICommand FindDirectoryToDecryptCommand { get; private set; }
        public ICommand EncryptCommand { get; private set; }
        public ICommand DecryptCommand { get; private set; }
        public ICommand EncryptDirectoryCommand { get; private set; }
        public ICommand DecryptDirectoryCommand { get; private set; }
        public ICommand ClearLogCommand { get; private set; }

        #endregion

        #region Private Localisation Fields

        private string? passwordRequiredLocalisation;
        private string? selectFileToEncryptLocalisation;
        private string? fileDoesNotExistLocalisation;
        private string? successfullyEncryptedFileLocalisation;
        private string? errorEncryptingFileLocalisation;
        private string? selectFileToDecryptLocalisation;
        private string? successfullyDecryptedFileLocalisation;
        private string? errorDecryptingFileLocalisation;
        private string? selectDirectoryToEncryptLocalisation;
        private string? successfullyEncryptedDirectoryLocalisation;
        private string? errorEncryptingDirectoryLocalisation;
        private string? selectDirectoryToDecryptLocalisation;
        private string? successfullyDecryptedDirectoryLocalisation;
        private string? errorDecryptingDirectoryLocalisation;
        private string? errorLocalisation;
        private string? passwordLocalisation;
        private string? singleFileModeLocalisation;
        private string? filePathLocalisation;
        private string? directoryModeLocalisation;
        private string? findLocalisation;
        private string? encryptLocalisation;
        private string? decryptLocalisation;
        private string? findDirectoryLocalisation;
        private string? encryptDirectoryLocalisation;
        private string? decryptDirectoryLocalisation;
        private string? clearLogLocalisation;

        #endregion

        #region Public Localisation Properties

        public string? PasswordRequiredLocalisation
        {
            get { return passwordRequiredLocalisation; }
            set { SetProperty(ref passwordRequiredLocalisation, value); }
        }

        public string? SelectFileToEncryptLocalisation
        {
            get { return selectFileToEncryptLocalisation; }
            set { SetProperty(ref selectFileToEncryptLocalisation, value); }
        }

        public string? FileDoesNotExistLocalisation
        {
            get { return fileDoesNotExistLocalisation; }
            set { SetProperty(ref fileDoesNotExistLocalisation, value); }
        }

        public string? SuccessfullyEncryptedFileLocalisation
        {
            get { return successfullyEncryptedFileLocalisation; }
            set { SetProperty(ref successfullyEncryptedFileLocalisation, value); }
        }

        public string? ErrorEncryptingFileLocalisation
        {
            get { return errorEncryptingFileLocalisation; }
            set { SetProperty(ref errorEncryptingFileLocalisation, value); }
        }

        public string? SelectFileToDecryptLocalisation
        {
            get { return selectFileToDecryptLocalisation; }
            set { SetProperty(ref selectFileToDecryptLocalisation, value); }
        }

        public string? SuccessfullyDecryptedFileLocalisation
        {
            get { return successfullyDecryptedFileLocalisation; }
            set { SetProperty(ref successfullyDecryptedFileLocalisation, value); }
        }

        public string? ErrorDecryptingFileLocalisation
        {
            get { return errorDecryptingFileLocalisation; }
            set { SetProperty(ref errorDecryptingFileLocalisation, value); }
        }

        public string? SelectDirectoryToEncryptLocalisation
        {
            get { return selectDirectoryToEncryptLocalisation; }
            set { SetProperty(ref selectDirectoryToEncryptLocalisation, value); }
        }

        public string? SuccessfullyEncryptedDirectoryLocalisation
        {
            get { return successfullyEncryptedDirectoryLocalisation; }
            set { SetProperty(ref successfullyEncryptedDirectoryLocalisation, value); }
        }

        public string? ErrorEncryptingDirectoryLocalisation
        {
            get { return errorEncryptingDirectoryLocalisation; }
            set { SetProperty(ref errorEncryptingDirectoryLocalisation, value); }
        }

        public string? SelectDirectoryToDecryptLocalisation
        {
            get { return selectDirectoryToDecryptLocalisation; }
            set { SetProperty(ref selectDirectoryToDecryptLocalisation, value); }
        }

        public string? SuccessfullyDecryptedDirectoryLocalisation
        {
            get { return successfullyDecryptedDirectoryLocalisation; }
            set { SetProperty(ref successfullyDecryptedDirectoryLocalisation, value); }
        }

        public string? ErrorDecryptingDirectoryLocalisation
        {
            get { return errorDecryptingDirectoryLocalisation; }
            set { SetProperty(ref errorDecryptingDirectoryLocalisation, value); }
        }

        public string? ErrorLocalisation
        {
            get { return errorLocalisation; }
            set { SetProperty(ref errorLocalisation, value); }
        }

        public string? PasswordLocalisation
        {
            get { return passwordLocalisation; }
            set { SetProperty(ref passwordLocalisation, value); }
        }

        public string? SingleFileModeLocalisation
        {
            get { return singleFileModeLocalisation; }
            set { SetProperty(ref singleFileModeLocalisation, value); }
        }

        public string? FilePathLocalisation
        {
            get { return filePathLocalisation; }
            set { SetProperty(ref filePathLocalisation, value); }
        }

        public string? DirectoryModeLocalisation
        {
            get { return directoryModeLocalisation; }
            set { SetProperty(ref directoryModeLocalisation, value); }
        }

        public string? FindLocalisation
        {
            get { return findLocalisation; }
            set { SetProperty(ref findLocalisation, value); }
        }

        public string? EncryptLocalisation
        {
            get { return encryptLocalisation; }
            set { SetProperty(ref encryptLocalisation, value); }
        }

        public string? DecryptLocalisation
        {
            get { return decryptLocalisation; }
            set { SetProperty(ref decryptLocalisation, value); }
        }

        public string? FindDirectoryLocalisation
        {
            get { return findDirectoryLocalisation; }
            set { SetProperty(ref findDirectoryLocalisation, value); }
        }

        public string? EncryptDirectoryLocalisation
        {
            get { return encryptDirectoryLocalisation; }
            set { SetProperty(ref encryptDirectoryLocalisation, value); }
        }

        public string? DecryptDirectoryLocalisation
        {
            get { return decryptDirectoryLocalisation; }
            set { SetProperty(ref decryptDirectoryLocalisation, value); }
        }

        public string? ClearLogLocalisation
        {
            get { return clearLogLocalisation; }
            set { SetProperty(ref clearLogLocalisation, value); }
        }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            FindFileToEncryptCommand = new DelegateCommand(FindFileToEncryptCommandHandler);
            FindFileToDecryptCommand = new DelegateCommand(FindFileToDecryptCommandHandler);
            FindDirectoryToEncryptCommand = new DelegateCommand(FindDirectoryToEncryptCommandHandler);
            FindDirectoryToDecryptCommand = new DelegateCommand(FindDirectoryToDecryptCommandHandler);
            EncryptCommand = new DelegateCommand(EncryptCommandHandler);
            DecryptCommand = new DelegateCommand(DecryptCommandHandler);
            EncryptDirectoryCommand = new DelegateCommand(EncryptDirectoryCommandHandler);
            DecryptDirectoryCommand = new DelegateCommand(DecryptDirectoryCommandHandler);
            ClearLogCommand = new DelegateCommand(ClearLogCommandHandler);

            Log = new ObservableCollection<string>();

            GetLocalisations();
        }

        #endregion

        #region Methods

        private void GetLocalisations()
        {
            PasswordRequiredLocalisation = Localisations.PasswordRequiredLocalisation;
            SelectFileToEncryptLocalisation = Localisations.SelectFileToEncryptLocalisation;
            FileDoesNotExistLocalisation = Localisations.FileDoesNotExistLocalisation;
            SuccessfullyEncryptedFileLocalisation = Localisations.SuccessfullyEncryptedFileLocalisation;
            ErrorEncryptingFileLocalisation = Localisations.ErrorEncryptingFileLocalisation;
            SelectFileToDecryptLocalisation = Localisations.SelectFileToDecryptLocalisation;
            SuccessfullyDecryptedFileLocalisation = Localisations.SuccessfullyDecryptedFileLocalisation;
            ErrorDecryptingFileLocalisation = Localisations.ErrorDecryptingFileLocalisation;
            SelectDirectoryToEncryptLocalisation = Localisations.SelectDirectoryToEncryptLocalisation;
            SuccessfullyEncryptedDirectoryLocalisation = Localisations.SuccessfullyEncryptedDirectoryLocalisation;
            ErrorEncryptingDirectoryLocalisation = Localisations.ErrorEncryptingDirectoryLocalisation;
            SelectDirectoryToDecryptLocalisation = Localisations.SelectDirectoryToDecryptLocalisation;
            SuccessfullyDecryptedDirectoryLocalisation = Localisations.SuccessfullyDecryptedDirectoryLocalisation;
            ErrorDecryptingDirectoryLocalisation = Localisations.ErrorDecryptingDirectoryLocalisation;
            ErrorLocalisation = Localisations.ErrorLocalisation;
            PasswordLocalisation = Localisations.PasswordLocalisation;
            SingleFileModeLocalisation = Localisations.SingleFileModeLocalisation;
            FilePathLocalisation = Localisations.FilePathLocalisation;
            DirectoryModeLocalisation = Localisations.DirectoryModeLocalisation;
            FindLocalisation = Localisations.FindLocalisation;
            EncryptLocalisation = Localisations.EncryptLocalisation;
            DecryptLocalisation = Localisations.DecryptLocalisation;
            FindDirectoryLocalisation = Localisations.FindDirectoryLocalisation;
            EncryptDirectoryLocalisation = Localisations.EncryptDirectoryLocalisation;
            DecryptDirectoryLocalisation = Localisations.DecryptDirectoryLocalisation;
            ClearLogLocalisation = Localisations.ClearLogLocalisation;
        }

        private void FindFileToEncryptCommandHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileToEncryptFilePath = openFileDialog.FileName;
            }
        }

        private void FindFileToDecryptCommandHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Encrypted Files (*.enc)|*.enc";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileToDecryptFilePath = openFileDialog.FileName;
            }
        }

        private void FindDirectoryToEncryptCommandHandler()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    DirectoryToEncrypt = folderBrowserDialog.SelectedPath;

                }
            }
        }

        private void FindDirectoryToDecryptCommandHandler()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    DirectoryToDecrypt = folderBrowserDialog.SelectedPath;

                }
            }
        }

        private void EncryptCommandHandler()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ShowError(PasswordRequiredLocalisation);
                return;
            }
            if (string.IsNullOrWhiteSpace(FileToEncryptFilePath))
            {
                ShowError(SelectFileToEncryptLocalisation);
                return;
            }
            if (!File.Exists(FileToEncryptFilePath))
            {
                ShowError(FileDoesNotExistLocalisation);
                return;
            }
            try
            {
                EncryptionManager.EncryptFile(FileToEncryptFilePath, Password);
                LogMessage(string.Format(SuccessfullyEncryptedFileLocalisation, FileToEncryptFilePath));
            }
            catch (FileExistsException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                LogMessage(string.Format(ErrorEncryptingFileLocalisation, FileToEncryptFilePath));
                ShowError(ex.Message);
            }
        }

        private void DecryptCommandHandler()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ShowError(PasswordRequiredLocalisation);
                return;
            }
            if (string.IsNullOrWhiteSpace(FileToDecryptFilePath))
            {
                ShowError(SelectFileToDecryptLocalisation);
                return;
            }
            if (!File.Exists(FileToDecryptFilePath))
            {
                ShowError(FileDoesNotExistLocalisation);
                return;
            }
            try
            {
                EncryptionManager.DecryptFile(FileToDecryptFilePath, Password);
                LogMessage(string.Format(SuccessfullyDecryptedFileLocalisation, FileToDecryptFilePath));
            }
            catch (FileExistsException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                LogMessage(string.Format(ErrorDecryptingFileLocalisation, FileToDecryptFilePath));
                ShowError(ex.Message);
            }
        }

        private void EncryptDirectoryCommandHandler()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ShowError(PasswordRequiredLocalisation);
                return;
            }
            if (string.IsNullOrWhiteSpace(DirectoryToEncrypt))
            {
                ShowError(SelectDirectoryToEncryptLocalisation);
                return;
            }
            try
            {
                EncryptionManager.EncryptDirectory(DirectoryToEncrypt, Password);
                LogMessage(string.Format(SuccessfullyEncryptedDirectoryLocalisation, DirectoryToEncrypt));
            }
            catch (FileExistsException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                LogMessage(string.Format(ErrorEncryptingDirectoryLocalisation, DirectoryToEncrypt));
                ShowError(ex.Message);
            }
        }

        private void DecryptDirectoryCommandHandler()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                ShowError(PasswordRequiredLocalisation);
                return;
            }
            if (string.IsNullOrWhiteSpace(DirectoryToDecrypt))
            {
                ShowError(SelectDirectoryToDecryptLocalisation);
                return;
            }
            try
            {
                EncryptionManager.DecryptDirectory(DirectoryToDecrypt, Password);
                LogMessage(string.Format(SuccessfullyDecryptedDirectoryLocalisation, DirectoryToDecrypt));
            }
            catch (FileExistsException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                LogMessage(string.Format(ErrorDecryptingDirectoryLocalisation, DirectoryToDecrypt));
                ShowError(ex.Message);
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message,
                            ErrorLocalisation,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        private void ClearLogCommandHandler()
        {
            Log?.Clear();
        }

        private void LogMessage(string message)
        {
            Log?.Add(string.Format("{0} {1}", DateTime.Now, message));
        }

        #endregion
    }
}
