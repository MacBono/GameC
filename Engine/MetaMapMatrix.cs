using System;
using System.Collections.Generic;
using Game.Engine.Interactions;
using Game.Engine.Interactions.InteractionFactories;
using Game.Engine.Skills;

namespace Game.Engine
{
    [Serializable]
    class MetaMapMatrix
    {
        private GameSession parentSession;
        // how many maps in total in the game world
        private const int maps = 25;
        private int minPortals = 35;
        // connections between maps
        private int[,] adjacencyMatrix = new int[maps, maps];
        private int[] visited;
        private int lastNumber;
        private int currentNumber;
        // interactions
        private int shops = 20; // number of shops in the game world
        private int interactions = 30; // number of all interactions (including shops) in the game world (can be slightly bigger due to quest constraints)
        private List<Interaction> interactionList;
        // maps
        private MapMatrix[] matrix;

        public MetaMapMatrix(GameSession parent)
        {
            parentSession = parent;
            // create adjacency matrix
            Random rng = new Random();
            for (int i = 0; i < minPortals; i++)
            {
                int a = rng.Next(maps);
                int b = rng.Next(maps);
                if (a == b || adjacencyMatrix[a, b] == 1) { i--; continue; }
                adjacencyMatrix[a, b] = 1;
                adjacencyMatrix[b, a] = 1;
            }
            while (!CheckConnectivity())
            {
                int a = rng.Next(maps);
                int b = rng.Next(maps);
                if (a == b || adjacencyMatrix[a, b] == 1) continue;
                adjacencyMatrix[a, b] = 1;
                adjacencyMatrix[b, a] = 1;
            }
            // generate interactions
            GenerateInteractions();
            // create maps
            matrix = new MapMatrix[maps];
            int totalIntNumber = interactionList.Count;
            for (int i = 0; i < maps; i++)
            {
                List<Interaction> tmp;
                if (i == maps - 1)  tmp = interactionList;
                else
                {
                    tmp = new List<Interaction>();
                    for (int u = 0; u < (totalIntNumber / maps + 1); u++)
                    {
                        if (interactionList.Count == 0) break;
                        int index = rng.Next(interactionList.Count);
                        tmp.Add(interactionList[index]);
                        interactionList.RemoveAt(index);
                    }
                }
                matrix[i] = new MapMatrix(parentSession, MakePortalsList(i), tmp, rng.Next(1000 * maps));
            }
        }

        public MapMatrix GetCurrentMatrix(int codeNumber)
        {
            lastNumber = currentNumber;
            currentNumber = codeNumber;
            return matrix[codeNumber];
        }
        public int GetPreviousMatrixCode()
        {
            // for display when portal hopping
            return lastNumber;
        }
        private bool CheckConnectivity()
        {
            // check if the adjacencyMatrix represents a fully connected graph
            visited = new int[maps];
            SearchAndMark(0);
            for (int i = 0; i < maps; i++)
            {
                if (visited[i] == 0) return false;
            }
            return true;
        }
        private void SearchAndMark(int nodeNumber)
        {
            // recursive function
            visited[nodeNumber] = 1;
            for (int i = 0; i < maps; i++)
            {
                if (visited[i] == 1) continue;
                if (adjacencyMatrix[i, nodeNumber] == 0) continue;
                SearchAndMark(i);
            }
        }
        private List<int> MakePortalsList(int node)
        {
            // utility method for converting from matrix to list of portals
            List<int> ans = new List<int>();
            for (int i = 0; i < maps; i++)
            {
                if (adjacencyMatrix[i, node] == 1) ans.Add(i);
            }
            return ans;
        }
        private void GenerateInteractions()
        {
            interactionList = new List<Interaction>();
            for (int i = 0; i < shops; i++) interactionList.Add(new ShopInteraction(parentSession));
            for (int i = shops; i < interactions; i++) interactionList.AddRange(Index.DrawInteractions(parentSession));
        }
    }
}
