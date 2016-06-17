$Classes(MoM.Module.Dtos*Dto)[$Properties[$Imports]
export interface $Name {$Properties[
    $name: $Type;]
}]
${
    using Typewriter.Extensions.Types;
	string Imports(Property property){
		var type = property.Type;
		if (type.IsPrimitive)
			return string.Empty;
		return "import {" + type.ToString().Replace("[", string.Empty).Replace("]", string.Empty) + "} from \"./" + property.Type.ToString().Replace("[", string.Empty).Replace("]", string.Empty) + "\";" + Environment.NewLine;
	}
    Template(Settings settings)
    {
        settings
            .IncludeCurrentProject()
            .IncludeProject("MoM.Module");
    }
}