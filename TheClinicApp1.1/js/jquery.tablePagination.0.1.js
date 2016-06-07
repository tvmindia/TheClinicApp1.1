/**
 * tablePagination - A table plugin for jQuery that creates pagination elements
 *
 * http://neoalchemy.org/tablePagination.html
 *
 * Copyright (c) 2009 Ryan Zielke (neoalchemy.com)
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 *
 * @name tablePagination
 * @type jQuery
 * @param Object settings;
 *      firstArrow - Image - Pass in an image to replace default image. Default: (new Image()).src="../images/First.png"
 *      prevArrow - Image - Pass in an image to replace default image. Default: (new Image()).src="../images/Previous.png"
 *      lastArrow - Image - Pass in an image to replace default image. Default: (new Image()).src="../images/Last.png"
 *      nextArrow - Image - Pass in an image to replace default image. Default: (new Image()).src="../images/Next.png"
 *      rowsPerPage - Number - used to determine the starting rows per page. Default: 5
 *      currPage - Number - This is to determine what the starting current page is. Default: 1
 *      optionsForRows - Array - This is to set the values on the rows per page. Default: [7,14,21,50,100]
 *      ignoreRows - Array - This is to specify which 'tr' rows to ignore. It is recommended that you have those rows be invisible as they will mess with page counts. Default: []
 *
 *
 * @author Ryan Zielke (neoalchemy.org)
 * @version 0.1.0
 * @requires jQuery v1.2.3 or above
 */

 (function($){

     $.fn.tablePagination = function (settings) {
         debugger;
		var defaults = {  
		    firstArrow: (new Image()).src = "../images/First.png",
		    prevArrow: (new Image()).src = "../images/Previous.png",
			lastArrow: (new Image()).src = "../images/Last.png",
			nextArrow: (new Image()).src = "../images/Next.png",
			rowsPerPage : 7,
			currPage : 1,
			optionsForRows : [7,14,21,50],
			ignoreRows: [],
			table: '',
			rowCountstart: 1,
			rowCountend:7
		};  
		settings = $.extend(defaults, settings);
		
		return this.each(function () {
		    debugger;
      var table = $(this)[0];
      var totalPagesId = '#'+table.id+'+#tablePagination #tablePagination_totalPages';
      var currPageId = '#'+table.id+'+#tablePagination #tablePagination_currPage';
      var rowsPerPageId = '#'+table.id+'+#tablePagination #tablePagination_rowsPerPage';
      var firstPageId = '#'+table.id+'+#tablePagination #tablePagination_firstPage';
      var prevPageId = '#'+table.id+'+#tablePagination #tablePagination_prevPage';
      var nextPageId = '#'+table.id+'+#tablePagination #tablePagination_nextPage';
      var lastPageId = '#'+table.id+'+#tablePagination #tablePagination_lastPage';
      
      var possibleTableRows = $.makeArray($('tbody tr', table));
      var tableRows = $.grep(possibleTableRows, function(value, index) {
        return ($.inArray(value, defaults.ignoreRows) == -1);
      }, false)
      
      var numRows = tableRows.length
      var totalPages = resetTotalPages();
      var currPageNumber = (defaults.currPage > totalPages) ? 1 : defaults.currPage;
      if ($.inArray(defaults.rowsPerPage, defaults.optionsForRows) == -1)
        defaults.optionsForRows.push(defaults.rowsPerPage);
      
      
      function hideOtherPages(pageNum) {
          debugger;
        if (pageNum==0 || pageNum > totalPages)
          return;
        var startIndex = (pageNum - 1) * defaults.rowsPerPage;
        var endIndex = (startIndex + defaults.rowsPerPage - 1);
        $(tableRows).show();
        for (var i=0;i<tableRows.length;i++) {
          if (i < startIndex || i > endIndex) {
            $(tableRows[i]).hide()
          }
        }
      }
      
      function resetTotalPages() {
          debugger;
        var preTotalPages = Math.round(numRows / defaults.rowsPerPage);
        var totalPages = (preTotalPages * defaults.rowsPerPage < numRows) ? preTotalPages + 1 : preTotalPages;
        if ($(totalPagesId).length > 0)
          $(totalPagesId).html(totalPages);
        return totalPages;
      }
      
      function resetCurrentPage(currPageNum) {
          
        if (currPageNum < 1 || currPageNum > totalPages)
          return;
        currPageNumber = currPageNum;
        hideOtherPages(currPageNumber);
        $(currPageId).val(currPageNumber)
      }
      
      function resetPerPageValues() {
         
        var isRowsPerPageMatched = false;
        var optsPerPage = defaults.optionsForRows;
        optsPerPage.sort();
        var perPageDropdown = $(rowsPerPageId)[0];
        perPageDropdown.length = 0;
        for (var i=0;i<optsPerPage.length;i++) {
          if (optsPerPage[i] == defaults.rowsPerPage) {
            perPageDropdown.options[i] = new Option(optsPerPage[i], optsPerPage[i], true, true);
            isRowsPerPageMatched = true;
          }
          else {
            perPageDropdown.options[i] = new Option(optsPerPage[i], optsPerPage[i]);
          }
        }
        if (!isRowsPerPageMatched) {
          defaults.optionsForRows == optsPerPage[0];
        }
      }
      
      function createPaginationElements() {
          debugger;
          var htmlBuffer = [];         
        htmlBuffer.push("<div id='tablePagination'>");
        htmlBuffer.push("<span id='tablePagination_perPage'>");
        htmlBuffer.push("<select id='tablePagination_rowsPerPage'><option value='7'>7</option></select>");
        htmlBuffer.push(" Per Page");
        htmlBuffer.push("</span>");
        htmlBuffer.push("<span id='tablePagination_paginater'>");
        htmlBuffer.push("Records of");
        htmlBuffer.push(" " + defaults.rowCountstart + " - <span id='tablePagination_totalPages'>" + defaults.rowCountend + "</span>");
        htmlBuffer.push("<span id='tablePagination_totalRows'> of " + numRows + "</span>")
        htmlBuffer.push("<img id='tablePagination_firstPage' src='"+defaults.firstArrow+"'>");
        htmlBuffer.push("<img id='tablePagination_prevPage' src='"+defaults.prevArrow+"'>");
        
        //htmlBuffer.push("<input id='tablePagination_currPage' type='input' value='"+currPageNumber+"' size='1'>");
        
        htmlBuffer.push("<img id='tablePagination_nextPage' src='"+defaults.nextArrow+"'>");
        htmlBuffer.push("<img id='tablePagination_lastPage' src='"+defaults.lastArrow+"'>");
        htmlBuffer.push("</span>");
        htmlBuffer.push("</div>");
        
        return htmlBuffer.join("").toString();
        
        
      }
      
      
      if ($(totalPagesId).length == 0) {
          $(this).after(createPaginationElements());
          if(numRows<=defaults.rowCountend)
          {
              $('#tablePagination').hide();
          }
      }
      else {
        $('#tablePagination_currPage').val(currPageNumber);
      }
      resetPerPageValues();
      hideOtherPages(currPageNumber);
      bindoncemore();
      function bindoncemore() {
          $(firstPageId).bind('click', function (e) {
              $('#tablePagination').remove();
              defaults.rowCountstart =1;
              defaults.rowCountend =7;
              resetCurrentPage(1) 
              $('#' + table.id).after(createPaginationElements());
              bindoncemore();
          });

          $(prevPageId).bind('click', function (e) {
              debugger;
              if ((defaults.rowCountstart != '1') && (defaults.rowCountend != defaults.rowsPerPage)) {
                  $('#tablePagination').remove();
                  resetCurrentPage(currPageNumber -= 1)
                  defaults.rowCountstart -= defaults.rowsPerPage;
                  defaults.rowCountend -= defaults.rowsPerPage;
                  if (defaults.rowCountend <= 7) {

                      resetCurrentPage(1)
                      defaults.rowCountstart = 1;
                      defaults.rowCountend = 7;
                  }
                  debugger;
                  $('#' + table.id).after(createPaginationElements());
              }
                  bindoncemore();
                  bindoncemore();
              
              
          });

          $(nextPageId).bind('click', function (e) {
              $('#tablePagination').remove();
              resetCurrentPage(currPageNumber += 1)
              defaults.rowCountstart += defaults.rowsPerPage;
              defaults.rowCountend += defaults.rowsPerPage;
              
              if (defaults.rowCountend < numRows)
              {
                  defaults.rowCountend = defaults.rowCountend;
              }
              else
              {
                  defaults.rowCountend = numRows;
              }                               
              $('#' + table.id).after(createPaginationElements());                                                                         
                  bindoncemore();              
              if(defaults.rowCountend==numRows)
              {
                  $('#tablePagination_nextPage').remove();
                  defaults.rowCountend = numRows;
                  
              }

          });

          $(lastPageId).bind('click', function (e) {
              $('#tablePagination').remove();
              defaults.rowCountstart = 1;
              defaults.rowCountend = numRows;
              resetCurrentPage(totalPages)
              $('#' + table.id).after(createPaginationElements());
              bindoncemore();
          });

          $(currPageId).bind('change', function (e) {
              resetCurrentPage(this.value)
              bindoncemore();
          });

          $(rowsPerPageId).bind('change', function (e) {
              defaults.rowsPerPage = parseInt(this.value, 10);
              totalPages = resetTotalPages();
              resetCurrentPage(1)
              bindoncemore();
          });
      }
		})
	};		
})(jQuery);