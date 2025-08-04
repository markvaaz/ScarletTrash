using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using ScarletCore;
using ScarletCore.Services;
using ScarletCore.Systems;
using ScarletCore.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Collections;
using Unity.Entities;

namespace ScarletTrash;

[HarmonyPatch]
internal static class InventoryPatch {
  public static Dictionary<Entity, ActionId> RunningActions = [];
  private static readonly float DelaySeconds = 15f;
  [HarmonyPatch(typeof(ReactToInventoryChangedSystem), nameof(ReactToInventoryChangedSystem.OnUpdate))]
  [HarmonyPrefix]
  public static void Prefix(ReactToInventoryChangedSystem __instance) {
    if (!GameSystems.Initialized) return;
    var entities = __instance.__query_2096870026_0.ToEntityArray(Allocator.Temp);

    try {
      foreach (var entity in entities) {
        var changeEvent = entity.Read<InventoryChangedEvent>();
        var inventory = changeEvent.InventoryEntity;

        if (!inventory.Has<NameableInteractable>() && inventory.Has<Attach>()) {
          inventory = inventory.Read<Attach>().Parent;
        }

        if (!inventory.Has<NameableInteractable>() || !Regex.IsMatch(inventory.Read<NameableInteractable>().Name.Value, @"\bSTrash\b")) continue;

        if (RunningActions.TryGetValue(inventory, out var actionId)) {
          ActionScheduler.CancelAction(actionId);
          RunningActions.Remove(inventory);
        }
        RunningActions.Add(inventory, ActionScheduler.Delayed(() => {
          if (inventory.Exists()) {
            InventoryService.ClearInventory(inventory);
          }
          RunningActions.Remove(inventory);
        }, DelaySeconds));
      }
    } finally {
      entities.Dispose();
    }
  }

  [HarmonyPatch(typeof(NameableInteractableSystem), nameof(NameableInteractableSystem.OnUpdate))]
  [HarmonyPrefix]
  public static void Prefix(NameableInteractableSystem __instance) {
    var query = __instance._RenameQuery.ToEntityArray(Allocator.Temp);
    var niem = GameSystems.NetworkIdSystem._NetworkIdLookupMap._NetworkIdToEntityMap;

    foreach (var entity in query) {
      var renameEvent = entity.Read<InteractEvents_Client.RenameInteractable>();

      if (!Regex.IsMatch(renameEvent.NewName.Value, @"\bSTrash\b")) continue;

      var fromCharacter = entity.Read<FromCharacter>();

      if (!fromCharacter.Character.IsPlayer()) continue;

      var player = fromCharacter.Character.GetPlayerData();

      player.SendMessage("**~⚠~ Container renamed to trash bin! ~⚠~**".FormatError());
      player.SendMessage("Items placed inside will be ~permanently deleted~ after ~15 seconds~ of no activity.".FormatError());
      player.SendMessage("Timer resets each time you move items. There is ~no undo~!".FormatError());
    }
  }
}