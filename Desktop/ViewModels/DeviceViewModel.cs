﻿
using System.ComponentModel;

namespace Desktop.ViewModels
{
    // Cette classe gere le formatage des donnee cote vue ainsi que les evenements associes

    public class DeviceViewModel : INotifyPropertyChanged
    {
        // Infos sur le DD
        private double _totalROMSpace;
        private double _freeROMSpace;
        private double _usedROMSpace;

        // Infos sur la RAM
        private double _totalRAMSpace;
        private double _freeRAMSpace;
        private double _usedRAMSpace;

        public DeviceViewModel() { }

        public DeviceViewModel(double totalROMSpace, double freeROMSpace) 
        {
            _totalROMSpace = totalROMSpace;
            _freeRAMSpace = freeROMSpace;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Evenement sur les infos de la ROM
        public double TotalROMSpace
        {
            get { return _freeROMSpace; }
            set
            {
                _totalROMSpace = value;
                OnPropertyChanged(nameof(TotalROMSpace));
            }
        }

        public double FreeROMSpace
        {
            get { return _freeROMSpace; }
            set
            {
                _freeROMSpace = value;
                OnPropertyChanged(nameof(FreeROMSpace));
            }
        }

        public double UsedROMSpace
        {
            get { return _usedROMSpace; }
            set
            {
                _usedROMSpace = value;
                OnPropertyChanged(nameof(UsedROMSpace));
            }
        }

        // Evenement sur les infos de la RAM
        public double FreeRAMSpace
        {
            get { return _freeRAMSpace; }
            set
            {
                _freeROMSpace = value;
                OnPropertyChanged(nameof(FreeRAMSpace));
            }
        }

        public double UsedRAMSpace
        {
            get { return _usedRAMSpace; }
            set
            {
                _usedRAMSpace = value;
                OnPropertyChanged(nameof(UsedRAMSpace));
            }
        }
    }
}
