using Microsoft.AspNetCore.Mvc;
using System;

namespace Task_12.Helpers
{
    public class NameHelper
    {
        private const string _asyncPostfix = "Async";

        public static string GetControllerName(string controllerFullName)
        {
            if (string.IsNullOrWhiteSpace(controllerFullName))
            {
                throw new ArgumentException($"{nameof(controllerFullName)} is null or white space.", nameof(controllerFullName));
            }

            if (!controllerFullName.Contains(nameof(Controller)))
            {
                throw new ArgumentException($"'{controllerFullName}' is not controller name.");
            }

            return controllerFullName.Replace(nameof(Controller), string.Empty);
        }

        public static string GetActionName(string actionFullName)
        {
            if (string.IsNullOrWhiteSpace(actionFullName))
            {
                throw new ArgumentException($"{nameof(actionFullName)} is null or white space.", nameof(actionFullName));
            }

            return actionFullName.Replace(_asyncPostfix, string.Empty);
        }

        public static string GetRouteName(string controllerFullName, string actionFullName)
        {
            if (string.IsNullOrWhiteSpace(controllerFullName))
            {
                throw new ArgumentException($"{nameof(controllerFullName)} is null or white space.", nameof(controllerFullName));
            }

            if (!controllerFullName.Contains(nameof(Controller)))
            {
                throw new ArgumentException($"'{controllerFullName}' is not controller name.");
            }

            if (string.IsNullOrWhiteSpace(actionFullName))
            {
                throw new ArgumentException($"{nameof(actionFullName)} is null or white space.", nameof(actionFullName));
            }

            return $"{GetControllerName(controllerFullName)}_{GetActionName(actionFullName)}";
        }

    }
}
