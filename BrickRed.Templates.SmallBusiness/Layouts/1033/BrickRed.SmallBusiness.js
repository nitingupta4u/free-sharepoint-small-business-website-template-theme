function FixRibbonAndWorkspaceDimensions() {
    ULSxSy: ;
    g_frl = true;
    var elmRibbon = GetCachedElement("s4-ribbonrow");
    var elmWorkspace = GetCachedElement("s4-workspace");
    var elmTitleArea = GetCachedElement("s4-titlerow");
    var elmBodyTable = GetCachedElement("s4-bodyContainer");
    if (!elmRibbon || !elmWorkspace || !elmBodyTable) {
        return;
    }
    if (!g_setWidthInited) {
        var setWidth = true;
        if (elmWorkspace.className.indexOf("s4-nosetwidth") > -1)
            setWidth = false;
        g_setWidth = setWidth;
        g_setWidthInited = true;
    }
    else {
        var setWidth = g_setWidth;
    }
    var baseRibbonHeight = RibbonIsMinimized() ? 44 : 135;
    var ribbonHeight = baseRibbonHeight + g_wpadderHeight;
    if (GetCurrentEltStyle(elmRibbon, "visibility") == "hidden") {
        ribbonHeight = 0;
    }
}