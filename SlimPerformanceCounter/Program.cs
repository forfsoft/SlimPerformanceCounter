using CounterHelper;
using System;
using System.Threading;

namespace SlimPerformanceCounter
{
	class Program
	{
		static private ICounterManager _cpu_manager = null;
		static void Main(string[] args)
		{
			Console.WriteLine("Ready");
			start();
			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(1000);
				addCounter();
			}
			stop();
		}

		static private void start()
		{
			if (null == _cpu_manager)
			{
				Options CPU_Processor_Time_Counter = new Options()
				{
					Name = "CPU Percent",
					CategoryName = "Processor",
					CounterName = "% Processor Time",
					InstanceName = "_Total",
					MachineName = ".",
					ReadOnly = true,
					IterationLength = 1000
				};
				_cpu_manager = new CounterManager(CPU_Processor_Time_Counter);
			}
			_cpu_manager.StartCounter();
			Console.WriteLine("StartCounter");
		}

		static private void stop()
		{
			_cpu_manager.StopCounter();
			Console.WriteLine("StopCounter");
		}

		static private void addCounter()
		{
			if (null == _cpu_manager || _cpu_manager.GetStartStopState() == true)
			{
				Console.WriteLine("Stop monitorring");
				return;
			}
			string value = _cpu_manager.GetCounterValue();
			Console.WriteLine(value);
		}

	}
}
