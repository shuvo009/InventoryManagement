$(function () {
    function createEmptyTile() {
        var imageDiv = $("<div data-tile-image></div>").addClass("MakeLeft LeftDivWidth");
        var textDiv = $("<div data-tile-text></div>").addClass("MakeLeft RightDivWidth");
        return $("<div></div>").addClass("slide").append(imageDiv, textDiv);
    }

    function getAttributeValue(dom, attrName) {
        var attrVal = $(dom).attr(attrName);
        if (attrVal)
            return attrVal;
        else
            return null;
    }

    function getImage(dom, attrName) {
        var imagePath = getAttributeValue(dom, attrName);
        if (imagePath)
            return imagePath;
        else {
            var firstImagePath = getAttributeValue(dom, "data-first-image");
            if (firstImagePath)
                return firstImagePath;
            else {
                var secondImagePath = getAttributeValue(dom, "data-second-image");
                if (secondImagePath)
                    return secondImagePath;
            }
        }
        return null;
    }

    function getText(dom, attrName) {
        var tileText = getAttributeValue(dom, attrName);
        if (tileText)
            return tileText;
        else {
            var firstTileText = getAttributeValue(dom, "data-first-text");
            if (firstTileText)
                return firstTileText;
            else {
                var secondTileText = getAttributeValue(dom, "data-second-text");
                if (secondTileText)
                    return secondTileText;
            }
        }
        return null;
    }

    function setValueInTitle(imagePath, titleText) {
        var emptyTile = createEmptyTile();
        $(emptyTile).children("div[data-tile-image]").css("background-image", "url('.." + imagePath + "')");
        $(emptyTile).children("div[data-tile-text]").html(titleText);
        return emptyTile;
    }

    function getFirstAttrs(dom) {
        var imagePath = getImage(dom, "data-first-image");
        var titleText = getText(dom, "data-first-text");
        $(dom).addClass("MainContener").append(setValueInTitle(imagePath, titleText));
    }

    function getSecondAttrs(dom) {
        var imagePath = getImage(dom, "data-second-image");
        var titleText = getText(dom, "data-second-text");
        $(dom).addClass("MainContener").append(setValueInTitle(imagePath, titleText));
    }

    $('div[data-MetroTile]').each(function () {
        getFirstAttrs($(this));
    });



    //getFirstAttrs($("#testDiv"));

    //getSecondAttrs($("#testDiv"));
});