@checkout
Feature: Checkout
    As a customer
    I want to complete the checkout process
    So that I can purchase my items

    Background:
        Given I am logged in as a standard user
        And I add "Sauce Labs Backpack" to the cart

    @regression
    Scenario: Complete checkout with valid information
        When I open the cart
        And I proceed to checkout
        And I fill checkout info with "Test" "User" "12345"
        And I continue checkout
        And I finish the order
        Then the order should be complete

    @regression
    Scenario: Checkout fails without first name
        When I open the cart
        And I proceed to checkout
        And I fill checkout info with "" "User" "12345"
        And I continue checkout
        Then I should see checkout error "First Name is required"

    @regression
    Scenario: Checkout fails without postal code
        When I open the cart
        And I proceed to checkout
        And I fill checkout info with "Test" "User" ""
        And I continue checkout
        Then I should see checkout error "Postal Code is required"
