/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function(config) {
    // Define changes to default configuration here.
    // For complete reference see:
    // http://docs.ckeditor.com/#!/api/CKEDITOR.config

    
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = "vi";
    config.filebrowserBrowseUrl = '/Assets/Plugin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Assets/Plugin/ckfinder.html?Type=Images';
    config.filebrowserFlashUploadUrl = '/Assets/Plugin/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Assets/Plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/Assets/Plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/Assets/Plugin/ckfinder/');

}