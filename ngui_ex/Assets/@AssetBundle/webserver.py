import time
import BaseHTTPServer

import json

HOST_NAME = '192.168.0.179' #'127.0.0.1' # !!!REMEMBER TO CHANGE THIS!!!
PORT_NUMBER = 9292 # Maybe set this to 9000.

class MyHandler(BaseHTTPServer.BaseHTTPRequestHandler):
    def do_HEAD(s):
        s.send_response(200)
        s.send_header("Content-type", "text/html")
        s.end_headers()
    def do_GET(s):

        if s.path.endswith("login"):                
            """Respond to a GET request."""
            s.send_response(200)
            s.send_header("Content-type", "text/json")
            s.end_headers()

            d = { "result" : "ok"}            
            s.wfile.write(json.dumps(d))
            s.wfile.close()

        elif s.path.endswith("info/patchdate"):
            s.send_response(200)
            s.send_header("Content-type", "text/json")
            s.end_headers()

            d = { "recent_patch_date" : "5/18/2016 4:43:00 PM"}            
            s.wfile.write(json.dumps(d))
            s.wfile.close()

        elif s.path.endswith("info/patchlist"):
            s.send_response(200)
            s.send_header("Content-type", "text/json")
            s.end_headers()

            #d = { "patch_list" : [ "test.unity3d", "test2.unity3d","test3.unity3d", "test4.unity3d", "test5.unity3d", "test6.unity3d", "test7.unity3d", "pnl_lobby.unity3d", "pnl_battle.unity3d"] }            
            d = { "patch_list" : [ "test.unity3d", "test2.unity3d","test3.unity3d", "test4.unity3d", "pnl_lobby.unity3d", "pnl_battle.unity3d"] } 
            s.wfile.write(json.dumps(d))
            s.wfile.close()
            
        else:
            s.send_response(404)
            s.send_header("Content-type", "text/json")
            s.end_headers()

            d = { "result" : "no"}            
            s.wfile.write(json.dumps(d))
            s.wfile.close()
    def do_POST(s):
        if s.path.endswith("login"):

            content_len = int(s.headers.getheader('content-length', 0))
            post_body = s.rfile.read(content_len)
            json_data = json.dumps(post_body)
            print "post_body %s" % (json_data)

            # "{ 'id' : 'hello', 'pw' : 'hello'}"
            #j = json.loads('{"id" : "hh", "pw" : "hh"}')
            #userID = j['id']
            #userPW = j['pw']
            
            #userID = json_data['id']
            #userPW = json_data['pw']
            #print "ID: %s" % (userID)
            #print "PW: %s" % (userPW)
            
            s.send_response(200)
            s.send_header('Access-Control-Allow-Credentials', 'true')
            s.send_header('Access-Control-Allow-Origin', 'http://localhost:9192')
            s.send_header("Content-type", "text/json")
            d = { "result" : "ok"}
            s.send_header("Content-length", str(len(json.dumps(d))))
            s.end_headers()

            s.wfile.write(json.dumps(d))
            s.wfile.close()
            
       
if __name__ == '__main__':
    server_class = BaseHTTPServer.HTTPServer
    httpd = server_class((HOST_NAME, PORT_NUMBER), MyHandler)
    print time.asctime(), "Server Starts - %s:%s" % (HOST_NAME, PORT_NUMBER)
    try:
        httpd.serve_forever()
    except KeyboardInterrupt:
        pass
    httpd.server_close()
    print time.asctime(), "Server Stops - %s:%s" % (HOST_NAME, PORT_NUMBER)
