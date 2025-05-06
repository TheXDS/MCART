# MCART.Mvvm
## Overview
MCART.Mvvm is a library designed to simplify the implementation of the MVVM (Model-View-ViewModel) pattern in .NET applications. It provides a set of base classes and interfaces that help developers create maintainable and testable applications by separating concerns between the UI and business logic.

## Features
- **Base Classes**: Provides several kinds of base classes for ViewModels and Models to reduce boilerplate code.
- **Command Implementation**: Offers a couple of either powerful or simple command implementations to handle user interactions in the View.

## Getting Started
**Create a ViewModel**: Create a class that inherits from `ViewModelBase` and implement the properties and commands you need.
```csharp
public class MyViewModel : ViewModelBase
{
	private string _name;
	public string Name
	{
		get => _name;
		set => Change(ref _name, value);
	}
	public ICommand SaveCommand { get; }
	public MyViewModel()
	{
		SaveCommand = new SimpleCommand(OnSave);
	}
	private void OnSave()
	{
		// Save logic here
	}
}
```
The `ViewModelBase` class and its derivates provide many protected and public methods to help with the implementation of the MVVM pattern. The `Change` method is used to notify the UI about property changes, and the `SimpleCommand` class is a simple implementation of the `ICommand` interface that can be used to handle user interactions in the View.