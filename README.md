# Scholar Village

An interactive learning environment constructed with a gameplay structure, web-hosting servers and MVC model

**Tested by multiple users -> Increased UPTIME by 45% with Amazon ELB load balancing and Cloudfare CDN. Optimized RUNTIME by 60% of the original build by continuously storing/retrieving large media files on Amazon S3 / adopting SGD (Stochastic Gradiant Descent) to periodically updating model parameters learning from a random subset of user feedback.

--Running Instruction

under react-ui folder run command "npm install"
under react-ui folder run command "npm i bootstrap"

For proper operation, the server must be started first as they both start on port: 3000 by default, the server has also been set to start on port 5000 and is proxied accordingly in the ui
