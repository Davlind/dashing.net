class Dashing.Tcbuildstatus extends Dashing.Widget

  @accessor 'percentage', Dashing.AnimatedValue

  constructor: ->
    super
    @observe 'percentage', (value) ->
      $(@node).find(".meter").val(value).trigger('change')

  ready: ->
    meter = $(@node).find(".meter")
    meter.attr("data-bgcolor", meter.css("background-color"))
    meter.attr("data-fgcolor", meter.css("color"))
    meter.knob()
    @meter = meter

  setStatus: (element, status) ->
    1
#    $(@node).find(element).attr 'class', (i,c) ->
#      c.replace /\bstatus-\S+/g, ''
#      # add new class
#    $(@node).find(element).addClass "status-" + status

    
  onData: (data) ->
    if data.status
      # clear existing "status-*" classes
      $(@get('node')).attr 'class', (i,c) ->
        c.replace /\bstatus-\S+/g, ''
      # add new class
      $(@get('node')).addClass "status-#{data.status}"
    
    if data.state == 'running'
      $(@node).find(".progress").css('visibility', 'visible')
    else
      $(@node).find(".progress").css('visibility', 'hidden')

    for status, i in data.historicStatus
#        i = 0
#        status = data.historicStatus[i]

        elem = $(@get('node')).find('.hist' + i)
        elem.attr 'class', (j,c) ->
          c.replace /\bstatus-\S+/g, ''
        # add new class
        elem.addClass "status-" + status



    
    0
  
#      $(@get('.meter')).attr 'data-bgcolor', (i,c) ->
#        c.replace /\bmeter-status-\S+/g, ''

      # add new class
#      $(@get('.meter')).addClass "meter-status-#{data.status#      


#      meter = $(@node).find(".meter")
#      meter.attr("data-bgcolor", meter.css("background-color"))

#      meter.attr("data-fgcolor", meter.css("color"))
#
#      meter.knob()


#      meter = $(@node).find(".meter")
#      if !data.percentage
#        meter.hide()
#      else
#        meter.show()
