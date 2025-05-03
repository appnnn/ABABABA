using HR_Application.Data;
using HR_Application.Model;
using HR_Application.Repositories;
using HR_Application.View.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace HR_Application.ViewModel
{
    public class EmployeeRegistrationViewModel : INotifyPropertyChanged
    {
        private Employee _employee = new Employee();
        private readonly IEmployeeRepository _employeeRepository;


        public EmployeeRegistrationViewModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository; // initialize the repository
            // Set default to today's date
            DateOfBirth = DateTime.Today;
            JoiningDate = DateTime.Today;
            SaveCommand = new RelayCommand(SaveEmployee);
        }

        // Properties for Binding
        public string FirstName
        {
            get => _employee.FirstName;
            set { _employee.FirstName = value; OnPropertyChanged(); }
        }
        public string LastName
        {
            get => _employee.LastName;
            set { _employee.LastName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _employee.Email;
            set { _employee.Email = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get => _employee.PhoneNumber;
            set { _employee.PhoneNumber = value; OnPropertyChanged(); }
        }

        public string Department
        {
            get => _employee.Department;
            set { _employee.Department = value; OnPropertyChanged(); }
        }

        public string Position
        {
            get => _employee.Position;
            set { _employee.Position = value; OnPropertyChanged(); }
        }

        public decimal Salary
        {
            get => _employee.Salary;
            set { _employee.Salary = value; OnPropertyChanged(); }
        }

        
        public DateTime DateOfBirth
        {
            get => _employee.DateOfBirth;
            set
            {
                if (_employee.DateOfBirth != value)
                {
                    _employee.DateOfBirth = value;
                    OnPropertyChanged();
                }
            }
        }
        //public DateTime DateOfBirth
        //{
        //    get => _employee.DateOfBirth;
        //    set { _employee.DateOfBirth = value; OnPropertyChanged(); }
        //}



        public DateTime JoiningDate
        {
            get => _employee.JoiningDate;
            set { _employee.JoiningDate = value; OnPropertyChanged(); }

        }

        //public string ProfilePicturePath
        //{
        //    get => _employee.ProfilePicturePath;
        //    set { _employee.ProfilePicturePath = value; OnPropertyChanged(); }
        //}

        //public string Username
        //{
        //    get => _employee.Username;
        //    set { _employee.Username = value; OnPropertyChanged(); }
        //}

        //public string Password
        //{
        //    get => _employee.Password;
        //    set { _employee.Password = value; OnPropertyChanged(); }
        //}

        public string Role
        {
            get => _employee.Role;
            set { _employee.Role = value; OnPropertyChanged(); }
        }

        public List<string> DepartmentsList => Departments.DepartmentsList;
        public List<string> RolesList => Roles.RolesList;
        public List<string> PositionsList => Positions.PositionsList;
        public List<string> GendersList => Genders.GenderList;
        public string Gender
        {
            get => _employee.Gender;
            set { _employee.Gender = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get => _employee.Address;
            set { _employee.Address = value; OnPropertyChanged(); }
        }

        // Command to Save Employee
        public ICommand SaveCommand { get; }

    
        private void SaveEmployee()
        {
            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Email) )
            {
                System.Windows.MessageBox.Show("Please fill in all required fields.", "Validation Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return;
            }

            _employee.Username = FirstName;
            string plainPassword = FirstName;
            // Encrypt password using BCrypt before storing
            _employee.Password = BCrypt.Net.BCrypt.HashPassword(plainPassword);

            try
            {
                //InsertEmployee.InsertEmployeeDB(_employee);
                _employeeRepository.AddEmployee(_employee);
                System.Windows.MessageBox.Show("Employee registered successfully!", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DB Error: {ex.Message}\n{ex.StackTrace}"); // DEBUG log
                throw new Exception($"Error inserting employee into the database. Details: {ex.Message}", ex);
            }
        }


        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}
