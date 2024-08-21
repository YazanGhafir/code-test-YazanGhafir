## Application Requirements

### Overview

Enhance and refine a .NET Core-based backend application for a tire change booking system. Your objective is to develop an application that is not only functional but also meets high standards of reliability, maintainability, and operational transparency.

### Functional Requirements

Welcome to our tire change booking system! This application facilitates the booking of tire changes for vehicles, ensuring a seamless and efficient experience for our customers. Here is an outline of the key components:

**Appointment Availability and Scheduling:**
- Bookings must be made at least 24 hours in advance and not more than 30 days ahead.
- Each appointment slot accommodates one vehicle.
- Operating hours are 8:00 AM - 5:00 PM, Monday to Friday, and 9:00 AM - 2:00 PM on Saturdays. Bookings outside these hours are not permitted.
- Customers should receive detailed and structured booking confirmations.
- (Optional) Cancellations are allowed up to 36 hours before the scheduled appointment.
- (Optional) A reminder email is sent to the customer the day before their appointment.
- (Optional) To minimize loss from unutilized slots, if a cancellation occurs, the next customer booking the same slot receives a 50% discount.

**Pricing:**
- Pricing is dynamic and varies based on car type, tire size, and optional services:
  - **Car Type:** Sedans ($100), SUVs ($120), Trucks ($150), Other ($90).
  - **Tire Size:** ≤16" ($20 fee), 17"-18" ($40 surcharge), >18" ($60 additional cost).
  - **Optional Services:** Services like Wheel Alignment and Balancing add to the total cost.
- A loyalty discount of 10% on the total cost is available for returning customers.

**Slot Availability *(External service to integrate with)*:**
- The slot availability service manages and displays Hedin's available appointment times at our workshop. Use this service for booking tire changes. Some slots may be outside opening hours - and thus not available for tire change. 
- For accessing the Slots API you have been given an tenant id in your appsettings file. Please treat this id as a secret.

These components are designed to work together harmoniously, ensuring a smooth and efficient tire change process for our customers.

### Non-Functional Requirements

1. **Code Quality and Structure:**
   - Ensure the code is well-organized, readable, and follows standard coding conventions.
   - Use patterns and practices that enhance maintainability and scalability.

2. **Performance and Scalability:**
   - Optimize application performance for varying loads.
   - Design the application to support increasing demand and future requirements.

3. **Security and Safety:**
   - Protect against common security vulnerabilities and ensure data integrity.

4. **Understanding and Transparency:**
   - Make the application’s processes and decisions easy to understand and trace.

5. **Verification and Assurance:**
   - Prove that the application fulfills all stated requirements.

### Additional Notes
- The current solution requires enhancements, refactoring, and further implementations to fully meet the outlined requirements.
- Documenting your development approach and choices is highly valued and contributes to the overall assessment of your submission.
- [Slot Availability Service Swagger](https://slots.hedinit.io/swagger/index.html)
- **Slots service url:** `https://slots.hedinit.io/v1/`


## Tenant Ids for your application *(your ID to use for communication with slot api)*: 
- TenantId is: **DD957EE1-173E-450C-8AB3-C521106A4587w** for dev
- TenantId is: **00886881-7919-4CA4-9553-C94916ED77B2w** for prod
