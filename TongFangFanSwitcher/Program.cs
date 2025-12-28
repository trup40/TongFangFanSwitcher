using System;
using System.Management;
using System.Threading;

namespace TongFangFanSwitcher
{
    class Program
    {
        // EC Memory address for fan speed: 1873 (0x751)
        // Normal speed value: 27648 (0x6C00)
        // Turbo speed value: 27712 (0x6C40)

        const UInt64 FAN_ADDRESS = 1873UL;
        const string NORMAL_SPEED = "27648";
        const UInt64 TURBO_VALUE = 64UL;
        const UInt64 NORMAL_VALUE = 0UL;

        static void Main(string[] args)
        {
            Console.WriteLine("TongFang GM7MP0P Fan Switcher v1.0");
            Console.WriteLine("===================================");
            Console.WriteLine("Reading current fan mode...");

            var currentValue = WMIReadECRAM(FAN_ADDRESS);

            if (currentValue == null)
            {
                Console.WriteLine("\nError: Could not read EC memory.");
                Console.WriteLine("Please make sure you run as Administrator.");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }

            Console.WriteLine($"Current value: {currentValue}");

            if (currentValue == NORMAL_SPEED)
            {
                Console.WriteLine("Switching to TURBO mode...");
                if (WMIWriteECRAM(FAN_ADDRESS, TURBO_VALUE))
                {
                    Console.WriteLine("✓ Successfully switched to TURBO mode!");
                }
            }
            else
            {
                Console.WriteLine("Switching to NORMAL mode...");
                if (WMIWriteECRAM(FAN_ADDRESS, NORMAL_VALUE))
                {
                    Console.WriteLine("✓ Successfully switched to NORMAL mode!");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static string? WMIReadECRAM(UInt64 Addr)
        {
            try
            {
                ManagementObject classInstance = new ManagementObject(
                    "root\\WMI",
                    "AcpiTest_MULong.InstanceName='ACPI\\PNP0C14\\1_1'",
                    null);

                ManagementBaseObject inParams = classInstance.GetMethodParameters("GetSetULong");

                Addr = 0x10000000000 + Addr;
                inParams["Data"] = Addr;

                Thread.Sleep(200); // Workaround to avoid EC busy flag

                ManagementBaseObject outParams = classInstance.InvokeMethod("GetSetULong", inParams, null);

                return outParams["Return"].ToString();
            }
            catch (ManagementException err)
            {
                Console.WriteLine("WMI Read failed: " + err.Message);
                return null;
            }
        }

        static bool WMIWriteECRAM(UInt64 Addr, UInt64 Value)
        {
            try
            {
                ManagementObject classInstance = new ManagementObject(
                    "root\\WMI",
                    "AcpiTest_MULong.InstanceName='ACPI\\PNP0C14\\1_1'",
                    null);

                ManagementBaseObject inParams = classInstance.GetMethodParameters("GetSetULong");

                Value = Value << 16;
                Addr = Value + Addr;
                inParams["Data"] = Addr;

                Thread.Sleep(200); // Workaround to avoid EC busy flag

                classInstance.InvokeMethod("GetSetULong", inParams, null);

                return true;
            }
            catch (ManagementException err)
            {
                Console.WriteLine("WMI Write failed: " + err.Message);
                return false;
            }
        }
    }
}