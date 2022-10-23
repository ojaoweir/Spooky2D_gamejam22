using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
namespace IsakUtils
{
    public static class UnityTagConstants
    {
        // Default Tags
        public const string UNTAGGED = "Untagged";
        public const string RESPAWN = "Respawn";
        public const string FINISH = "Finish";
        public const string EDITORONLY = "EditorOnly";
        public const string MAINCAMERA = "MainCamera";
        public const string PLAYER = "Player";
        public const string GAMECONTROLLER = "GameController";

        //Project specific tags
        public const string EXAMPLE = "Example";


        public static string[] getAllTagConstants()
        {
            var tagConstants = new List<string>();
            var defaultTags = new string[] {
            UNTAGGED,
            RESPAWN,
            FINISH,
            EDITORONLY,
            MAINCAMERA,
            PLAYER,
            GAMECONTROLLER,
        };

            var projectSpecificTags = new string[] {
            //EXAMPLE,
        };

            tagConstants.AddRange(defaultTags);
            tagConstants.AddRange(projectSpecificTags);
            return tagConstants.ToArray();
        }
    }
}