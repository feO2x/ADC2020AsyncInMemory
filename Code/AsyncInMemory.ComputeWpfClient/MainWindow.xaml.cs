﻿using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
// ReSharper disable All

namespace AsyncInMemory.ComputeWpfClient
{
    public sealed partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ProgressRing.Visibility = Visibility.Collapsed;
        }

        private void CalculateOnUiThread(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ToTextBox.Text, out var parsedNumber) || parsedNumber < 3)
            {
                ResultTextBlock.Text = "Please enter a valid number greater than 2.";
                return;
            }

            Button.IsEnabled = false;
            ProgressRing.Visibility = Visibility.Visible;
            ResultTextBlock.Text = null;

            var lowestCommonMultiple = Math.CalculateLowestCommonMultiple(parsedNumber);

            Button.IsEnabled = true;
            ProgressRing.Visibility = Visibility.Collapsed;
            ResultTextBlock.Text = $"The lowest common multiple of all numbers from 2 to {parsedNumber} is {lowestCommonMultiple}.";
        }

        private async void CalculateOnBackgroundThread(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ToTextBox.Text, out var parsedNumber) || parsedNumber < 3)
            {
                ResultTextBlock.Text = "Please enter a valid number greater than 2.";
                return;
            }

            Button.IsEnabled = false;
            ProgressRing.Visibility = Visibility.Visible;
            ResultTextBlock.Text = null;

            var lowestCommonMultiple = await Math.CalculateLowestCommonMultipleAsync(parsedNumber);

            Button.IsEnabled = true;
            ProgressRing.Visibility = Visibility.Collapsed;
            ResultTextBlock.Text = $"The lowest common multiple of all numbers from 2 to {parsedNumber} is {lowestCommonMultiple}.";
        }

        // When an async method is generated by the compiler, it is transformed into something similar than
        // the following method. Of course, it is only present in IL. This C# version is reconstructed from IL.
        private void CalculateOnBackgroundThreadDecompiled(object sender, RoutedEventArgs e)
        {
            var machine = new AsyncStateMachine
            {
                Builder = AsyncTaskMethodBuilder<long>.Create(),
                State = -1,
                ToTextBox = ToTextBox,
                Button = Button,
                ProgressRing = ProgressRing,
                ResultTextBlock = ResultTextBlock
            };
            machine.Builder.Start(ref machine);
        }

        private struct AsyncStateMachine : IAsyncStateMachine
        {
            public TextBox ToTextBox;
            public Button Button;
            public ProgressRing ProgressRing;
            public TextBlock ResultTextBlock;
            public int ParsedNumber;
            public AsyncTaskMethodBuilder<long> Builder;
            public TaskAwaiter<long> TaskAwaiter;
            public int State; // -2 = done (successful or exception caught), -1 = running, other states for different await statements

            public void MoveNext()
            {
                if (State == -2)
                    return;

                if (State == 0)
                    goto GetResultFromTaskAwaiter;

                if (!int.TryParse(ToTextBox.Text, out ParsedNumber) || ParsedNumber < 3)
                {
                    ResultTextBlock.Text = "Please enter a valid number greater than 2.";
                    State = -2;
                    return;
                }

                Button.IsEnabled = false;
                ProgressRing.Visibility = Visibility.Visible;
                ResultTextBlock.Text = null;

                var localTaskAwaiter = Math.CalculateLowestCommonMultipleAsync(ParsedNumber).GetAwaiter();
                if (localTaskAwaiter.IsCompleted)
                    goto Continuation;

                State = 0;
                TaskAwaiter = localTaskAwaiter;
                Builder.AwaitOnCompleted(ref localTaskAwaiter, ref this);
                return;

                GetResultFromTaskAwaiter:
                localTaskAwaiter = TaskAwaiter;
                TaskAwaiter = default;
                State = -1;

                Continuation:
                var lowestCommonMultiple = localTaskAwaiter.GetResult();

                Button.IsEnabled = true;
                ProgressRing.Visibility = Visibility.Collapsed;
                ResultTextBlock.Text = $"The lowest common multiple of all numbers from 2 to {ParsedNumber} is {lowestCommonMultiple}.";
                State = -2;
            }

            public void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                Builder.SetStateMachine(stateMachine);
            }
        }
    }
}