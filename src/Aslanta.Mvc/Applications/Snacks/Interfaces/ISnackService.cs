using System;

namespace Aslanta.Snacks.Interfaces;

public interface ISnackService
{
    Task<Snack> GetSnackAsync();
}
