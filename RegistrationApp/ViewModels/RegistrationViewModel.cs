using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using RegistrationApp.ViewModels;

public class RegistrationViewModel : INotifyPropertyChanged
{
    private string _name;
    private string _email;
    private string _password;
    private string _confirmPassword;

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(nameof(Name)); }
    }

    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(nameof(Email)); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(nameof(Password)); }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
    }

    public ICommand RegisterCommand { get; }

    public RegistrationViewModel()
    {
        RegisterCommand = new RelayCommand(Register);
    }

    private void Register()
    {
        if (IsValidData())
        {
            MessageBox.Show("Registration successful!");
        }
        else
        {
            MessageBox.Show("Please fill out the form correctly.");
        }
    }

    private bool IsValidData()
    {
        bool isValid = true;

        if (string.IsNullOrEmpty(Name))
        {
            MessageBox.Show("Name is required.");
            isValid = false;
        }
        if (!IsValidEmail(Email))
        {
            MessageBox.Show("Email is incorrect.");
            isValid = false;
        }
        if (!IsValidPassword(Password))
        {
            MessageBox.Show("Password should be at least 6 characters long, contain at least one uppercase letter and one digit.");
            isValid = false;
        }
        if (Password != ConfirmPassword)
        {
            MessageBox.Show("Passwords do not match.");
            isValid = false;
        }

        return isValid;
    }

    private bool IsValidEmail(string email)
    {
        // Регулярное выражение для проверки email
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 6 &&
               Regex.IsMatch(password, @"[A-Z]") && // наличие заглавной буквы
               Regex.IsMatch(password, @"[0-9]");   // наличие цифры
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}