using HR_Application.Model;
using HR_Application.Repositories;
using HR_Application.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HR_Application.ViewModel
{

    public class EmployeeManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EmployeeDisplayModel> _employeeDisplay;
        private readonly IEmployeeRepository _employeeRepository;

        public ObservableCollection<EmployeeDisplayModel> EmployeeDisplay
        {
            get => _employeeDisplay;
            set
            {
                _employeeDisplay = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadCommand { get; }
        public ICommand AddEmployeeCommand { get; }

        public EmployeeManagementViewModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            LoadCommand = new RelayCommand(LoadEmployees);
            AddEmployeeCommand = new RelayCommand(AddEmployee);

            // Optionally load immediately
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            var employeeDisplayModel = EmployeeMapper.MapEmployeesToEmployeeDisplayModel(employees);
            EmployeeDisplay = new ObservableCollection<EmployeeDisplayModel>(employeeDisplayModel);
            
        }

        private void AddEmployee()
        {
            var registrationWindow = new EmployeeRegistrationWindow();
            registrationWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
