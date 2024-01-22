'use strict';
$(document).ready(function () {

  // Accordion
  var all_panels = $('.templatemo-accordion > li > ul').hide();

  $('.templatemo-accordion > li > a').click(function () {
    console.log('Hello world!');
    var target = $(this).next();
    if (!target.hasClass('active')) {
      all_panels.removeClass('active').slideUp();
      target.addClass('active').slideDown();
    }
    return false;
  });
  // End accordion

  // Product detail
  $('.product-links-wap a').click(function () {
    var this_src = $(this).children('img').attr('src');
    $('#product-detail').attr('src', this_src);
    return false;
  });

  // Increment quantity for a specific product
  $('.btn-plus').click(function () {
    var productId = $(this).data('product-id');
    var varValue = $("#var-value-" + productId);
    $('[id^="var-value-"]').not(varValue).text(0);
    var val = parseInt(varValue.text());
    val++;

   $('input[name="adet"]').val(val);

      $('input[name="choosenid"]').val(productId);

    varValue.text(val);
    $("#product-quantity-" + productId).val(val);

    return false;
  });

  // Decrement quantity for a specific product
  $('.btn-minus').click(function () {
    var productId = $(this).data('product-id');
    var varValue = $("#var-value-" + productId);

    var val = parseInt(varValue.text());
    val = (val > 0) ? val - 1 : 0;

    varValue.text(val);
    $("#product-quantity-" + productId).val(val);

    return false;
  });

  $('.btn-size').click(function () {
    var this_val = $(this).html();
    $("#product-size").val(this_val);
    $(".btn-size").removeClass('btn-secondary');
    $(".btn-size").addClass('btn-success');
    $(this).removeClass('btn-success');
    $(this).addClass('btn-secondary');
    return false;
  });
  // End product detail

});
