/*
PasswordDialogViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

namespace TheXDS.MCART.Dialogs.ViewModel
{
    internal class PasswordDialogViewModel : NotifyPropertyChanged
    {
        private PasswordDialogMode _mode;
        private string _user;
        private SecureString _password;
        private SecureString _confirm;
        private string _hint;
        private IPasswordEvaluator _evaluator;
        private PwEvalResult _result;
        private IPasswordGenerator _generator;
        private string _title;
        private bool _isBusy;

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
            }
        }

        public bool IsUserVisible => Mode.HasFlag(PasswordDialogMode.User);
        public bool IsConfirmVisible => Mode.HasFlag(PasswordDialogMode.Confirm);
        public bool IsHintVisible => Mode.HasFlag(PasswordDialogMode.Hint);
        public bool IsSecurityVisible => Mode.HasFlag(PasswordDialogMode.Security);
        public bool IsGeneratorVisible => Mode.HasFlag(PasswordDialogMode.Generator);

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

        public SecureString Password
        {
            get => _password;
            set
            {
                if (Equals(value, _password)) return;
                _password = value;
                OnPropertyChanged();
                Eval();
            }
        }

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

        public IPasswordEvaluator Evaluator
        {
            get => _evaluator;
            set
            {
                if (Equals(value, _evaluator)) return;
                _evaluator = value;
                OnPropertyChanged();
                Eval();
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

        public IPasswordGenerator Generator
        {
            get => _generator;
            set
            {
                if (Equals(value, _generator)) return;
                _generator = value;
                OnPropertyChanged();
                Generate();
            }
        }

        private void Eval()
        {
            if (!Mode.HasFlag(PasswordDialogMode.Security)) return;
            Result = Password.Length == 0 ? PwEvalResult.Empty : Evaluator?.Evaluate(Password) ?? PwEvalResult.Null;
        }

        public float PasswordQuality => Result.Result * 100;
        public string MorInfo => Result.Details;
        public bool IsInvalid => Result.Critical;

        public string GeneratedPassword { get; private set; }

        public void Generate()
        {
            if (!Mode.HasFlag(PasswordDialogMode.Generator)) return;
            Password = Generator?.Generate();
            Confirm = Password;
            GeneratedPassword = Password?.Read();
            OnPropertyChanged(nameof(GeneratedPassword));
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

        public int? MaxTries { get; set; }
        public int TriesCount { get; set; }
        public LoginValidator Validator { get; set; }
    }
}