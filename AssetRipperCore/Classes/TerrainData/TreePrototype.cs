using AssetRipper.Core.Project;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.YAML;
using System.Collections.Generic;

namespace AssetRipper.Core.Classes.TerrainData
{
	public struct TreePrototype : IAsset, IDependent
	{
		public void Read(AssetReader reader)
		{
			Prefab.Read(reader);
			BendFactor = reader.ReadSingle();
		}

		public void Write(AssetWriter writer)
		{
			Prefab.Write(writer);
			writer.Write(BendFactor);
		}

		public IEnumerable<PPtr<Object.Object>> FetchDependencies(DependencyContext context)
		{
			yield return context.FetchDependency(Prefab, PrefabName);
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add(PrefabName, Prefab.ExportYAML(container));
			node.Add(BendFactorName, BendFactor);
			return node;
		}

		public float BendFactor { get; set; }

		public const string PrefabName = "prefab";
		public const string BendFactorName = "bendFactor";

		public PPtr<GameObject.GameObject> Prefab;
	}
}