﻿using BattleBitAPI.Addons.CommandHandler.Common;

namespace BattleBitAPI.Addons.CommandHandler.Converters.TypeReaders;

public abstract class TypeReader<TPlayer> where TPlayer : Player
{
    protected TypeReader(Type type)
    {
        Type = type;
    }

    public Context<TPlayer> Context { get; internal set; }
    public Type Type { get; internal set; }

    public abstract object? ChangeType(object obj);
}