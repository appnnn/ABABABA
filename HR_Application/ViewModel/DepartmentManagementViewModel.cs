using HR_Application.Model;
using HR_Application.Repositories;
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
    public class DepartmentManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DepartmentModel> _departmentModel;
        private readonly IDepartmentRepository _departmentRepository;
        
        public ObservableCollection<DepartmentModel> DepartmentModel 
        { 
            get => _departmentModel; 
            set
            {
                _departmentModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadDepartmentCommand { get;  }

        //public DepartmentModel SelectedDepartment { get; set; }
        //public Employee SelectedEmployee { get; set; }

        //public ICommand AddDepartmentCommand { get; }
        //public ICommand UpdateDepartmentCommand { get; }
        //public ICommand DeleteDepartmentCommand { get; }
        //public ICommand AssignEmployeeCommand { get; }

        public DepartmentManagementViewModel(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;

            LoadDepartmentCommand = new RelayCommand(LoadDepartment);

            LoadDepartment();
            //Departments = new ObservableCollection<DepartmentModel>(_departmentRepository.GetAllDepartments());

            //AddDepartmentCommand = new RelayCommand(AddDepartment);
            //UpdateDepartmentCommand = new RelayCommand(UpdateDepartment);
            //DeleteDepartmentCommand = new RelayCommand(DeleteDepartment);
            //AssignEmployeeCommand = new RelayCommand(AssignEmployee);
        }

        public void LoadDepartment()
        {
            var departments = _departmentRepository.GetAllDepartments();
            DepartmentModel = new ObservableCollection<DepartmentModel>(departments);
        }

        //private void AddDepartment()
        //{
        //    var newDepartment = new Department { DepartmentName = "New Department", Description = "Description" };
        //    _departmentRepository.AddDepartment(newDepartment);
        //    Departments.Add(newDepartment);
        //}

        //private void UpdateDepartment()
        //{
        //    if (SelectedDepartment != null)
        //    {
        //        _departmentRepository.UpdateDepartment(SelectedDepartment);
        //    }
        //}

        //private void DeleteDepartment()
        //{
        //    if (SelectedDepartment != null)
        //    {
        //        _departmentRepository.DeleteDepartment(SelectedDepartment.DepartmentId);
        //        Departments.Remove(SelectedDepartment);
        //    }
        //}

        //private void AssignEmployee()
        //{
        //    if (SelectedDepartment != null && SelectedEmployee != null)
        //    {
        //        _departmentRepository.AssignEmployeeToDepartment(SelectedDepartment.DepartmentId, SelectedEmployee);
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
