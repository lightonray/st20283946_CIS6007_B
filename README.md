# Workers Circle Painting Project

This project demonstrates a multi-threaded approach to paint circles on a PictureBox in a Windows Forms application using C#. Each worker (thread) is assigned a range of circles to paint, and synchronization ensures that each circle is painted only once.

## Features

- **Multi-threaded Circle Painting**: Utilizes multiple threads to paint circles simultaneously.
- **Dynamic Circle Painting**: Circles are painted dynamically on a PictureBox based on its dimensions.
- **Thread Management**: Each worker thread paints a range of circles, avoiding conflicts and redundancies.
- **Visualization**: Displays the progress of circle painting and completion status.

## Getting Started

### Prerequisites

- Microsoft Visual Studio (or any C# IDE) installed.
- Windows Forms Application development knowledge.

### Running the Application

1. Clone or download the repository.
2. Open the solution file (`workers.sln`) in Visual Studio.
3. Build and run the project.

## Usage

- Upon launching the application, set the desired number of circles and workers using the UI.
- Click the "Start Painting Circles" button to initiate the circle painting process.
- Progress, completion status, and elapsed time will be displayed in the UI.

## File Structure

│
├── Form1.cs # Main form with UI controls and logic
├── Form1.Designer.cs # Designer-generated code for UI elements
├── Program.cs # Entry point of the application
└── workers.csproj # Project file

![Screenshot_20](https://github.com/lightonray/st20283946_CIS6007_B/assets/91954450/f870d795-8c3b-4268-bbe6-8c226a522509)

![Screenshot_21](https://github.com/lightonray/st20283946_CIS6007_B/assets/91954450/408c7a4a-44e3-4dfa-8fd7-f655e01779c3)



