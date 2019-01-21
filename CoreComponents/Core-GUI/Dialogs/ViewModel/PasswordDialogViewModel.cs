/*
PasswordDialogViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System.Security;
using System.Threading.Tasks;
using TheXDS.MCART.Security.Password;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.ViewModel;

namespace TheXDS.MCART.Dialogs.ViewModel
{
    internal class PasswordDialogViewModel : NotifyPropertyChanged
    {
        private SecureString _confirm;
        private IPasswordEvaluator _evaluator;
        private string _generatedPassword;
        private IPasswordGenerator _generator;
        private string _hint;
        private bool _isBusy;
        private PasswordDialogMode _mode;
        private SecureString _password;
        private PwEvalResult _result;
        private string _title;
        private string _user;
        private int _triesCount;
        private LoginValidator _validator;
        private int? _maxTries;

        private void OnEvaluate()
        {
            if (!Mode.HasFlag(PasswordDialogMode.Security)) return;
            Result = Password.Length == 0 ? PwEvalResult.Empty : Evaluator?.Evaluate(Password) ?? PwEvalResult.Null;
        }

        public SimpleCommand Evaluate { get; }
        public SimpleCommand Generate { get; }
        public bool IsConfirmVisible => Mode.HasFlag(PasswordDialogMode.Confirm);
        public bool IsGeneratorVisible => Mode.HasFlag(PasswordDialogMode.Generator);
        public bool IsHintVisible => Mode.HasFlag(PasswordDialogMode.Hint);
        public bool IsInvalid => Result.Critical;
        public bool IsSecurityVisible => Mode.HasFlag(PasswordDialogMode.Security);
        public bool IsUserVisible => Mode.HasFlag(PasswordDialogMode.User);
        public string MorInfo => Result.Details;
        public float PasswordQuality => Result.Result * 100;
        public SecureString Confirm
        {
            get => _confirm;
            set
            {
                if (Equals(value, _confirm)) return;
                _confirm = value;
                OnPropertyChanged();
            }
        }
        public IPasswordEvaluator Evaluator
        {
            get => _evaluator;
            set
            {
                if (Equals(value, _evaluator)) return;
                _evaluator = value;
                OnPropertyChanged();
                Evaluate.SetCanExecute(IsSecurityVisible && !(value is null));
                OnEvaluate();
            }
        }
        public string GeneratedPassword
        {
            get => _generatedPassword;
            private set
            {
                if (value == _generatedPassword) return;
                _generatedPassword = value;
                OnPropertyChanged();
            }
        }
        public IPasswordGenerator Generator
        {
            get => _generator;
            set
            {
                if (Equals(value, _generator)) return;
                _generator = value;
                OnPropertyChanged();
                Generate.SetCanExecute(IsGeneratorVisible && !(value is null));
                OnGenerate();
            }
        }
        public string Hint
        {
            get => _hint;
            set
            {
                if (value == _hint) return;
                _hint = value;
                OnPropertyChanged();
            }
        }
        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                if (value == _isBusy) return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public int? MaxTries
        {
            get => _maxTries;
            set
            {
                if (value == _maxTries) return;
                _maxTries = value;
                OnPropertyChanged();
            }
        }
        public PasswordDialogMode Mode
        {
            get => _mode;
            set
            {
                if (value == _mode) return;
                _mode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsUserVisible));
                OnPropertyChanged(nameof(IsConfirmVisible));
                OnPropertyChanged(nameof(IsHintVisible));
                OnPropertyChanged(nameof(IsSecurityVisible));
                OnPropertyChanged(nameof(IsGeneratorVisible));

                Evaluate.SetCanExecute(IsSecurityVisible && !(Evaluator is null));
                Generate.SetCanExecute(IsGeneratorVisible && !(Generator is null));
            }
        }
        public SecureString Password
        {
            get => _password;
            set
            {
                if (Equals(value, _password)) return;
                _password = value;
                OnPropertyChanged();
                OnEvaluate();
            }
        }
        public PwEvalResult Result
        {
            get => _result;
            private set
            {
                if (value.Equals(_result)) return;
                _result = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PasswordQuality));
                OnPropertyChanged(nameof(MorInfo));
                OnPropertyChanged(nameof(IsInvalid));
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }
        public int TriesCount
        {
            get => _triesCount;
            set
            {
                if (value == _triesCount) return;
                _triesCount = value;
                OnPropertyChanged();
            }
        }
        public string User
        {
            get => _user;
            set
            {
                if (value == _user) return;
                _user = value;
                OnPropertyChanged();
            }
        }
        public LoginValidator Validator
        {
            get => _validator;
            set
            {
                if (Equals(value, _validator)) return;
                _validator = value;
                OnPropertyChanged();
            }
        }
        public PasswordDialogViewModel()
        {
            Evaluate = new SimpleCommand(OnEvaluate, false);
            Generate = new SimpleCommand(OnGenerate, false);
        }

        public void OnGenerate()
        {
            if (!Mode.HasFlag(PasswordDialogMode.Generator)) return;
            Password = Generator?.Generate();
            Confirm = Password;
            GeneratedPassword = Password?.Read();
            OnPropertyChanged(nameof(GeneratedPassword));
        }
        public async Task<bool> ValidateAsync()
        {
            if (Password is null || Password.Length == 0) return false;
            if (Validator is null) return true;
            if (TriesCount > MaxTries) return false;
            TriesCount++;

            IsBusy = true;
            var t = await Validator(new Credential(User, Password));
            IsBusy = false;
            return t;
        }
    }
}