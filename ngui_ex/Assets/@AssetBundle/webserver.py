import time
import BaseHTTPServer

import json

# cd ~/prj/ngui_ex/ngui_ex/Assets/@AssetBundle
# python webserver.py
# python -m SimpleHTTPServer 9192

HOST_NAME = '192.168.0.151' #'127.0.0.1' # !!!REMEMBER TO CHANGE THIS!!!
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

        elif s.path.endswith("info/ios/patchlist"):
            s.send_response(200)
            s.send_header("Content-type", "text/json")
            s.end_headers()

            d = { "patch_list" : [ "iOS",
                                  "unlit - transparent colored.shader.unity3d",
                                  "atl_test_ui_set.png.unity3d",
                                  "tex_wooden atlas.png.unity3d",
                                  "tap.wav.unity3d",
                                  "test_ui_set.prefab.unity3d",
                                  "arimo20.prefab.unity3d",
                                  "mat_test_ui_set.mat.unity3d",
                                  "mat_wooden atlas.mat.unity3d",
                                  "wooden atlas.prefab.unity3d",
                                  "marble.mat.unity3d",
                                  "tex_bunny_eye.png.unity3d",
                                  "marble tile.psd.unity3d",
                                  "triplanar - bumped diffuse.shader.unity3d",
                                  "bunny eye.mat.unity3d",
                                  "marble tile nm.psd.unity3d",
                                  "stanford bunny.fbx.unity3d",
                                  "coalition.prefab.unity3d",
                                  "pnl_lobby.prefab.unity3d",
                                  "pnl_common_top_bar.prefab.unity3d"
                                   ] }

#d = { "patch_list" : [ "iOS", "pnl_lobby.prefab.unity3d", "unlit - transparent colored.shader.unity3d", "atl_test_ui_set.png.unity3d", "tex_wooden atlas.png.unity3d", "tap.wav.unity3d", "test_ui_set.prefab.unity3d", "arimo20.prefab.unity3d", "mat_test_ui_set.mat.unity3d", "mat_wooden atlas.mat.unity3d", "wooden atlas.prefab.unity3d","pnl_common_top_bar.prefab.unity3d","pnl_sham_battle_entrance.prefab.unity3d","pnl_map_choice.prefab.unity3d","pnl_chapter_map.prefab.unity3d", "pnl_stage_entrance.prefab.unity3d", "marble.mat.unity3d", "tex_bunny_eye.png.unity3d", "marble tile.psd.unity3d", "triplanar - bumped diffuse.shader.unity3d", "bunny eye.mat.unity3d", "marble tile nm.psd.unity3d", "stanford bunny.fbx.unity3d","pnl_party_edit.prefab.unity3d", "pnl_tween_item.prefab.unity3d", "coalition.prefab.unity3d", "pnl_formation_edit.prefab.unity3d","pnl_battle.prefab.unity3d","pnl_attack_party_edit.prefab.unity3d","pnl_defense_party_edit.prefab.unity3d","pnl_areana_intro_choreography.prefab.unity3d", "pnl_areana_battle.prefab.unity3d", "pnl_areana_ending_choreography.prefab.unity3d","pnl_character_info.prefab.unity3d", "pnl_formation_info.prefab.unity3d","pnl_areana_entrance.prefab.unity3d", "pnl_areana_ranking.prefab.unity3d", "pnl_other_user_party_info.prefab.unity3d", "pnl_areana_record.prefab.unity3d", "pnl_areana_record_review_check.prefab.unity3d", "pnl_areana_reward.prefab.unity3d", "pnl_item_info.prefab.unity3d", "pnl_areana_help.prefab.unity3d" ] }
                                  
#d = { "patch_list" : [ "iOS", "pnl_battle.unity3d", "tap.unity3d", "unlit - transparent colored.unity3d", "arimo20.unity3d", "wooden atlas.unity3d", "mat_wooden atlas.unity3d", "tex_wooden atlas.unity3d", "pnl_lobby.unity3d" ] }
#d = { "patch_list" : [ "iOS.manifest", "pnl_battle.unity3d", "tap.unity3d", "unlit - transparent colored.unity3d", "arimo20.unity3d", "wooden atlas.unity3d", "mat_wooden atlas.unity3d", "tex_wooden atlas.unity3d", "pnl_lobby.unity3d", "panel.unity3d", "atl_test_ui_set.unity3d", "test_ui_set.unity3d", "mat_test_ui_set.unity3d", "pnl_areana_battle.unity3d", "pnl_areana_cumulative.unity3d", "pnl_areana_ending_choreography.unity3d", "pnl_areana_entrance.unity3d", "pnl_areana_help.unity3d", "pnl_areana_intro_choreography.unity3d", "pnl_areana_ranking.unity3d", "pnl_areana_record.unity3d", "pnl_areana_record_review_check.unity3d", "pnl_areana_reward.unity3d", "pnl_attack_party_edit.unity3d", "pnl_change_party.unity3d", "pnl_chapter_map.unity3d", "start screen.unity3d", "pnl_character_info.unity3d", "pnl_common_top_bar.unity3d", "pnl_defense_party_edit.unity3d", "pnl_formation_edit.unity3d", "coalition.unity3d", "pnl_formation_info.unity3d", "pnl_item_info.unity3d", "pnl_map_choice.unity3d", "pnl_normal_popup.unity3d", "tap2.unity3d", "pnl_other_user_party_info.unity3d", "pnl_party_edit.unity3d", "pnl_sham_battle.unity3d", "pnl_stage_entrance.unity3d", "triplanar - bumped diffuse.unity3d", "marble tile.unity3d", "marble.unity3d", "bunny eye.unity3d", "marble tile nm.unity3d", "tex_bunny_eye.unity3d", "pnl_strongest_areana_entrance.unity3d", "pnl_strongest_areana_intro_choreography.unity3d", "pnl_strongest_areana_record_review_check.unity3d", "pnl_strongest_other_user_party_info.unity3d"] }
            s.wfile.write(json.dumps(d))
            s.wfile.close()
        
        elif s.path.endswith("info/android/patchlist"):
            s.send_response(200)
            s.send_header("Content-type", "text/json")
            s.end_headers()
            
            d = { "patch_list" : [ "Android", "pnl_battle.unity3d", "tap.unity3d", "unlit - transparent colored.unity3d", "arimo20.unity3d", "wooden atlas.unity3d", "mat_wooden atlas.unity3d", "tex_wooden atlas.unity3d", "pnl_lobby.unity3d" ] }
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
