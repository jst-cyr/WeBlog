(function ($) {
    $(document).ready(function () {
        // infinite scroll
        $('.wb-view-more', '#wb-view-more-wrapper').click(function () {
            var viewMore = $(this);
            var loading = viewMore.next(".wb-loading-animation");
            var entries = $("#wb-entry-list ul");
            viewMore.hide();
            loading.show();

            $.url.setUrl(document.location);
            var params = {
                startIndex: entries.children().length,
                blogAjax: 1
            };
            if ($.url.param("tag") != null) {
                params.tag = $.url.param("tag");
            }

            var url = $.url.setUrl(viewMore.attr("href")).attr("path");
            $.get(url, params, function (data) {
                var posts = jQuery(data).find('ul li');
                entries.append(posts);
                loading.hide();
                if (posts.length>0)
                    viewMore.show();
            });
            return false;
        });

        // archive toggle
        $('#wb-archive .wb-year, #wb-archive .wb-month').click(function () {
            var ctl = $(this);
            ctl.next('ul').toggle('200');
            ctl.toggleClass('expanded');
        });

        //tag cloud sort
        var sortOptions = $('.wb-tag-sorting li a');
        var sortDirection = {
            lastused: -1,
            alphabetic: 1,
            entrycount: -1
        }
        var sortTags = function (selectedSort) {
            var tags = $('.wb-tagCloud .wb-entries a');
            if (tags.length === 0) {
                return;
            }
            if (selectedSort.hasClass('wb-tag-sort-current')) {
                return;
            }
            var container = tags.first().parent();
            var sortOption = selectedSort.data('tag-sort');
            var direction = sortDirection[sortOption] ? sortDirection[sortOption] : 1;
            tags.sort(function (a, b) {
                a = $(a).data(sortOption);
                b = $(b).data(sortOption);
                return a > b ? direction : a < b ? (direction * -1) : 0;
            }).each(function () {
                container.append(this);
            });
            sortOptions.removeClass('wb-tag-sort-current');
            selectedSort.addClass('wb-tag-sort-current');
        }
        sortOptions.click(function() {
            sortTags($(this));
            return false;
        });
        var defaultSort = sortOptions.first();
        if (defaultSort) {
            sortTags(defaultSort);
        }
        if (sortOptions.length < 2) {
            $('.wb-tag-sorting').hide();
        }
    });
})(jQuery);