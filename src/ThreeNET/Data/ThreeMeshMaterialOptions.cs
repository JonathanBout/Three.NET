﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThreeNET.Data
{
	internal class ThreeMeshMaterialOptions
	{
		[JsonIgnore]
		public ThreeColor Color { get; set; } = new();

		[JsonPropertyName("color")]
		public string HexColorString
		{
			get
			{
				return Color.ToString();
			}
			set
			{
				ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
				value = value.Trim();
				if (value.StartsWith("#"))
				{
					value = value[1..];
					ParseHex();
				} else if (value.StartsWith("0x"))
				{
					value = value[2..];
					ParseHex();
				} else
				{
					Color = int.Parse(value);
				}

				void ParseHex()
				{
					Color = int.Parse(value, NumberStyles.HexNumber);
				}
			}
		}
	}
}
