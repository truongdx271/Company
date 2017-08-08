/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/assets/admin/js/plugin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/assets/admin/js/plugin/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/assets/admin/js/plugin/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/assets/admin/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/assets/admin/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    config.disallowedContent = 'script img p h1 h2 h3 pre strong';
    //config.removePlugins = 'image,table,tabletools,horizontalrule';
    //config.removeButtons = 'Anchor,Underline,Strike,Subscript,Superscript';
    //config.format_tags = 'p;h1;h2;pre';

    CKFinder.setupCKEditor(null, '/assets/admin/js/plugin/ckfinder/');


};

