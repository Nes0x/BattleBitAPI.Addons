namespace BattleBitAPI.Addons.EventHandler.Common;

public enum EventType
{
    /// <summary>
    /// Fired when a player kills another player.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnAPlayerKilledAnotherPlayer,
    
    /// <summary>
    /// Fired when a game server connects.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnGameServerConnected,
    
    /// <summary>
    /// Fired when an attempt made to connect to the server.<br/>
    /// Default, any connection attempt will be accepted
    /// </summary>
    /// <value>
    /// Returns: true if allow connection, false if deny the connection.
    /// You must return Task bool type.
    /// </value>        
    OnGameServerConnecting,
    
    /// <summary>
    /// Fired when a game server disconnects. Check (GameServer.TerminationReason) to see the reason.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnGameServerDisconnected,
    
    /// <summary>
    /// Fired when a game server reconnects. (When game server connects while a socket is already open)
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnGameServerReconnected,
    
    /// <summary>
    /// Fired when game server is ticking (~100hz)<br/>
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnGameServerTick,
    
    /// <summary>
    /// Fired when game server requests the stats of a player, this function should return in 3000ms or player will not able to join to server.
    /// </summary>
    /// <value>
    /// Returns: The modified stats of the player.
    /// You must return Task PlayerStats type.
    /// </value>  
    OnGetPlayerStats,
    
    /// <summary>
    /// Fired when a player changes their game role.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerChangedRole,
    
    /// <summary>
    /// Fired when a player changes team.
    /// </summary>
    OnPlayerChangedTeam,
    
    /// <summary>
    /// Fired when a player connects to a server.<br/>
    /// Check player.GameServer get the server that player joined.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerConnected,
    
    /// <summary>
    /// Fired when a player dies
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerDied,
    
    /// <summary>
    /// Fired when a player disconnects from a server.<br/>
    /// Check player.GameServer get the server that player left.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerDisconnected,
    
    /// <summary>
    /// Fired when a player joins a squad.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerJoinedASquad,
    
    /// <summary>
    /// Fired when a player leaves their squad.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerLeftSquad,
    
    /// <summary>
    /// Fired when a player reports another player.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerReported,
    
    /// <summary>
    /// Fired when a player requests server to change role.
    /// </summary>
    /// <value>
    /// Returns: True if you accept if, false if you don't.
    /// You must return Task bool type.
    /// </value>  
    OnPlayerRequestingToChangeRole,
    
    /// <summary>
    /// Fired when a player is spawns
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerSpawned,
    
    /// <summary>
    /// Fired when a player is spawning.
    /// </summary>
    /// <value>
    /// Returns: The new spawn response
    /// You must return Task PlayerSpawnRequest type.
    /// </value>  
    OnPlayerSpawning,
    
    /// <summary>
    /// Fired when a player types a message to text chat.<br/>
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnPlayerTypedMessage,
    
    /// <summary>
    /// Fired when game server requests to save the stats of a player.
    /// </summary>
    /// <value>
    /// You must return Task type.
    /// </value>  
    OnSavePlayerStats
}