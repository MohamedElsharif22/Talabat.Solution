$(document).ready(function () {
    // Variables to keep track of current filters and pagination
    let currentFilters = {
        brandId: '',
        categoryId: '',
        sort: '',
        search: '',
        pageIndex: 1,
        pageSize: 10
    };

    // Function to load products
    function loadProducts() {
        // Show loading indicator
        $('#loading').removeClass('d-none');

        // Make the AJAX request
        $.ajax({
            url: '/Products/GetProductsPartial',
            type: 'GET',
            data: currentFilters,
            success: function (data) {
                // Update the products container with the new HTML
                $('#productsContent').html(data);

                // Attach event handlers to pagination links
                attachPaginationHandlers();

                // Hide loading indicator
                $('#loading').addClass('d-none');
            },
            error: function () {
                $('#productsContent').html('<div class="alert alert-danger">Error loading products. Please try again.</div>');
                $('#loading').addClass('d-none');
            }
        });
    }

    // Handle form submission (search button click)
    $('#searchBtn').on('click', function () {
        // Reset to page 1 when applying new filters
        currentFilters.pageIndex = 1;

        // Update filter values from form
        currentFilters.brandId = $('#brandId').val();
        currentFilters.categoryId = $('#categoryId').val();
        currentFilters.sort = $('#sort').val();
        currentFilters.search = $('#search').val();

        // Load products with updated filters
        loadProducts();
    });

    // Auto-submit on filter change (optional)
    $('.filter-control').on('change', function () {
        $('#searchBtn').click();
    });

    // Function to attach handlers to pagination links
    function attachPaginationHandlers() {
        $('.page-nav-link').on('click', function () {
            // Update page index from the clicked link
            currentFilters.pageIndex = $(this).data('page');

            // Load products with updated page
            loadProducts();
        });
    }

    // Initial attachment of pagination handlers
    attachPaginationHandlers();
});